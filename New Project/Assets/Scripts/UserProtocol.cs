using System;
using UnityEngine;
using System.Collections.Generic;

namespace SQLClient
{
        public class User
        {
            public string username;
            public string pwd;

            public User(string username, string pwd)
            {
                this.username = username;
                this.pwd = pwd;
            }

            public string ToJSON()
            {
                return JsonUtility.ToJson(this);
            }
        }
    
}