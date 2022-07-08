using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Text;

namespace SQLClient
{
    public class Manager : MonoBehaviour
    {
        HttpClient client = new HttpClient();
        string baseUrl = "https://shltescaperoom.herokuapp.com";

        // get users
        public int GetUser(User newUser)
        {
            var formContent = new StringContent(newUser.ToJSON(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseUrl + "/api/login", formContent).GetAwaiter().GetResult();
            string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Debug.Log(responseStr);
            if (responseStr == "Logged in successfully!")
            {
                return 0;
            }
            else if (responseStr == "Invalid user")
            {
                return 1;
            }
            else if (responseStr == "Wrong password")
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        // create user
        public bool CreateUser(User newUser)
        {
            var formContent = new StringContent(newUser.ToJSON(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseUrl + "/api/register", formContent).GetAwaiter().GetResult();
            string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Debug.Log(responseStr);
            if (responseStr == "User created successfully!")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // modify user
        public bool ModifyUser(User user)
        {
            var formContent = new StringContent(user.ToJSON(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseUrl + "/api/resetpassword", formContent).GetAwaiter().GetResult();
            string responseStr = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Debug.Log(responseStr);
            if (responseStr == "User modified successfully!")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
