using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class User
{
    public string email;
    public string username;
    public string password;
    public string connection;
    public string client_id;

    public User(string email, string password)
    {
        this.email = email;
        this.password = password;
    }
}
