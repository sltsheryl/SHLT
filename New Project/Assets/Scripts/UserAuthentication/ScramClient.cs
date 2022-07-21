using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScramClient
{
    private readonly HttpClient client = new HttpClient();
    string baseUrl = "https://shltescaperoom.herokuapp.com/";

    private Dictionary<string, object> sessionDetails = new Dictionary<string, object>();

    public string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        byte[] data = new byte[4 * length];
        using (var crypto = RandomNumberGenerator.Create())
        {
            crypto.GetBytes(data);
        }
        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            long rnd = BitConverter.ToUInt32(data, i * 4);
            int idx = (int) (rnd % chars.Length);
            result.Append(chars[idx]);
        }
        return result.ToString();
    }

    public int LogIn(string username, string pwd)
    {
        sessionDetails.Clear();

        sessionDetails.Add("gs2", "n,,");
        sessionDetails.Add("cNonce", RandomString(32));
        // string gs2 = "n,,";
        // string cNonce = RandomString(32);
        string clientFirst = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{sessionDetails["gs2"]}n={username},r={sessionDetails["cNonce"]}"));

        HttpResponseMessage response;
        using (var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/api/login"))
        {
            // Sending client-first
            request.Headers.Authorization = new AuthenticationHeaderValue("SCRAM-SHA-256", $"data={clientFirst}");
            response = client.SendAsync(request).GetAwaiter().GetResult();
        }

        // Handling server-first
        IEnumerable<string> serverFirstValues;
        string serverFirst;
        if (response.Headers.TryGetValues("WWW-Authenticate", out serverFirstValues))
        {
            serverFirst = serverFirstValues.First();
        }
        else
        {
            return Constants.LOGIN_FAILED;
        }

        Dictionary<string, string> serverResponse = ParseAuthenticationHeader(serverFirst);
        if (serverResponse == null)
        {
            return Constants.LOGIN_FAILED;
        }
        sessionDetails.Add("sid", serverResponse["sid"]);
        // string sid = serverResponse["sid"];
        string serverData = serverResponse["serverData"];
        Dictionary<string, object> parsedServerFirst = ParseServerFirst(serverData);
        if (parsedServerFirst == null)
        {
            return Constants.LOGIN_FAILED;
        }
        // Casting is safe because of how the objects were added in ParseServerFirst
        sessionDetails.Add("nonce", parsedServerFirst["nonce"]);
        sessionDetails.Add("salt", parsedServerFirst["salt"]);
        sessionDetails.Add("i", parsedServerFirst["i"]);
        // string nonce = (string)parsedServerFirst["nonce"];
        // string salt = (string)parsedServerFirst["salt"];
        // int i = (int)parsedServerFirst["i"];
        string ClientFinal = HandleServerFirst(
            pwd,
            (string) sessionDetails["cNonce"], 
            (string) sessionDetails["gs2"], 
            (string) sessionDetails["nonce"], 
            (string) sessionDetails["salt"], 
            (int) sessionDetails["i"]
            );

        // Sending client-final
        using (var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + "/api/login"))
        {
            ClientFinal = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientFinal));
            request.Headers.Authorization = new AuthenticationHeaderValue("SCRAM-SHA-256", $"sid={sessionDetails["sid"]} data={ClientFinal}");
            response = client.SendAsync(request).GetAwaiter().GetResult();
        }

        // Handling server-final
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            IEnumerable<string> serverFinalValues;
            string serverFinal;
            if (response.Headers.TryGetValues("Authentication-Info", out serverFinalValues))
            {
                serverFinal = serverFinalValues.First();
            } 
            else
            {
                return Constants.LOGIN_FAILED;
            }
            serverResponse = ParseAuthenticationInfoHeader(serverFinal);
            if (serverResponse == null)
            {
                return Constants.LOGIN_FAILED;
            }

            if ((string) sessionDetails["sid"] != serverResponse["sid"])
            {
                return Constants.LOGIN_FAILED;
            }
            serverData = serverResponse["serverData"];
            Dictionary<string, object> parsedServerFinal = ParseServerFinal(serverData);
            if (parsedServerFinal == null)
            {
                return Constants.LOGIN_FAILED;
            }
            sessionDetails.Add("verifier", parsedServerFinal["verifier"]);
            bool authenticated = HandleServerFinal((string)sessionDetails["verifier"]);
            if (authenticated)
            {
                return Constants.LOGIN_SUCCESSFUL;
            }
            else
            {
                return Constants.LOGIN_FAILED;
            }
        }

        return Constants.LOGIN_FAILED;
    }

    public int RegisterUser(string username, string pwd)
    {
        string jsonForm = $"{{\"username\": \"{username}\", \"pwd\": \"{pwd}\"}}";
        var formContent = new StringContent(jsonForm, Encoding.UTF8, "application/json");
        Debug.Log(formContent.ToString());
        HttpResponseMessage response = client.PostAsync(baseUrl + "/api/register", formContent).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        if (responseStr == "User created successfully!")
        {
            return Constants.REGISTER_SUCCESSFUL;
        }
        else
        {
            return Constants.REGISTER_FAILED;
        }
    }

    public int ResetPassword(string username, string newPwd)
    {
        string jsonForm = $"{{\"username\": \"{username}\",\"pwd\": \"{newPwd}\"}}";
        var formContent = new StringContent(jsonForm, Encoding.UTF8, "application/json");
        HttpResponseMessage response = client.PostAsync(baseUrl + "/api/resetpassword", formContent).GetAwaiter().GetResult();
        string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        if (responseStr == "User modified successfully!")
        {
            return Constants.RESET_SUCCESSFUL;
        }
        else
        {
            return Constants.RESET_FAILED;
        }
    }

    private Dictionary<string, string> ParseAuthenticationHeader(string dataString)
    {
        string[] parts = dataString.Split(' ');
        if (parts.Length != 3)
        {
            return null;
        }

        if (parts[0] != "SCRAM-SHA-256" || !parts[1].StartsWith("sid=") || !parts[2].StartsWith("data="))
        {
            return null;
        }

        Regex serverDataPattern = new Regex(@"data=(?<data>[a-zA-Z0-9+/]+={0,2})", RegexOptions.Compiled);
        Regex serverSidPattern = new Regex(@"sid=(?<sid>[a-zA-Z0-9]{16})", RegexOptions.Compiled);

        MatchCollection sidMatch = serverSidPattern.Matches(parts[1]);
        MatchCollection serverDataMatch = serverDataPattern.Matches(parts[2]);
        
        if (sidMatch.Count != 1 || serverDataMatch.Count != 1)
        {
            return null;
        }
        string sid = sidMatch[0].Groups["sid"].Value;
        string serverData = serverDataMatch[0].Groups["data"].Value;
        Dictionary<string, string> result = new Dictionary<string, string>();
        result.Add("sid", sid);
        result.Add("serverData", serverData);

        return result;
    }

    private Dictionary<string, string> ParseAuthenticationInfoHeader(string dataString)
    {
        string[] parts = dataString.Split(' ');
        if (parts.Length != 2)
        {
            return null;
        }

        if (!parts[0].StartsWith("sid=") || !parts[1].StartsWith("data="))
        {
            return null;
        }

        Regex serverDataPattern = new Regex(@"data=(?<data>[a-zA-Z0-9+/]+={0,2})", RegexOptions.Compiled);
        Regex serverSidPattern = new Regex(@"sid=(?<sid>[a-zA-Z0-9]{16})", RegexOptions.Compiled);

        MatchCollection sidMatch = serverSidPattern.Matches(parts[0]);
        MatchCollection serverDataMatch = serverDataPattern.Matches(parts[1]);

        if (sidMatch.Count != 1 || serverDataMatch.Count != 1)
        {
            return null;
        }
        string sid = sidMatch[0].Groups["sid"].Value;
        string serverData = serverDataMatch[0].Groups["data"].Value;
        Dictionary<string, string> result = new Dictionary<string, string>();
        result.Add("sid", sid);
        result.Add("serverData", serverData);

        return result;
    }

    private Dictionary<string, object> ParseServerFirst(string serverData)
    {
        Regex serverFirstPattern = new Regex(@"r=(?<nonce>[a-zA-Z0-9]+%[a-zA-Z0-9]+),s=(?<salt>[a-zA-Z0-9]+),i=(?<i>\d+)", RegexOptions.Compiled);

        serverData = Encoding.UTF8.GetString(Convert.FromBase64String(serverData));
        MatchCollection serverFirstMatch = serverFirstPattern.Matches(serverData);

        if (serverFirstMatch.Count != 1)
        {
            return null;
        }

        string nonce = serverFirstMatch[0].Groups["nonce"].Value;
        string salt = serverFirstMatch[0].Groups["salt"].Value;
        int i = int.Parse(serverFirstMatch[0].Groups["i"].Value);
        Dictionary<string, object> result = new Dictionary<string, object>();
        result.Add("nonce", nonce);
        result.Add("salt", salt);
        result.Add("i", i);

        return result;
    }

    private string HandleServerFirst(string pwd, string cNonce, string gs2, string nonce, string salt, int i) {
        byte[] SaltedPassword = KeyDerivation.Pbkdf2(pwd, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA256, i, 32);
        byte[] ClientKey;
        using (HMACSHA256 hmac = new HMACSHA256(SaltedPassword))
        {
            ClientKey = hmac.ComputeHash(Encoding.UTF8.GetBytes("Client Key"));
            sessionDetails.Add("ServerKey", hmac.ComputeHash(Encoding.UTF8.GetBytes("Server Key")));
        }
        string AuthString = $"r={cNonce},r={nonce},s={salt},i={i},c={Convert.ToBase64String(Encoding.UTF8.GetBytes(gs2))},r={nonce}";
        sessionDetails.Add("AuthString", AuthString);

        byte[] ClientProof;
        using (SHA256 sha256 = SHA256.Create())
        {
            using (HMACSHA256 hmac = new HMACSHA256(sha256.ComputeHash(ClientKey)))
            {
                ClientProof = ExclusiveOr(ClientKey, hmac.ComputeHash(Encoding.UTF8.GetBytes(AuthString)));
            }  
        }
        string ClientFinal = $"c={Convert.ToBase64String(Encoding.UTF8.GetBytes(gs2))},r={nonce},p={Convert.ToBase64String(ClientProof)}";
        return ClientFinal;
    }

    private Dictionary<string, object> ParseServerFinal(string serverData)
    {
        Regex serverFinalPattern = new Regex(@"v=(?<verifier>[a-zA-Z0-9+/]+={0,2})", RegexOptions.Compiled);

        serverData = Encoding.UTF8.GetString(Convert.FromBase64String(serverData));
        MatchCollection serverFirstMatch = serverFinalPattern.Matches(serverData);

        if (serverFirstMatch.Count != 1)
        {
            return null;
        }

        string verifier = serverFirstMatch[0].Groups["verifier"].Value;
        Dictionary<string, object> result = new Dictionary<string, object>();
        result.Add("verifier", verifier);

        return result;
    }

    private bool HandleServerFinal(string verifier)
    {
        byte[] ServerKey = (byte[]) sessionDetails["ServerKey"];
        string AuthString = (string) sessionDetails["AuthString"];
        byte[] ExpectedSignature;
        using (HMACSHA256 hmac = new HMACSHA256(ServerKey))
        {
            ExpectedSignature = hmac.ComputeHash(Encoding.UTF8.GetBytes(AuthString));
        }
        return verifier == Convert.ToBase64String(ExpectedSignature);
    }


    private byte[] ExclusiveOr(byte[] a, byte[] b)
    {
        byte[] result = new byte[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            result[i] = (byte) (a[i] ^ b[i]);
        }
        return result;
    }
}
