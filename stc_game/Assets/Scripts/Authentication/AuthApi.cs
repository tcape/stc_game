using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;

public class AuthApi : MonoBehaviour {

    [Serializable]
	class User {
        public string connection;
        public string email;
        public string username;
        public string password;
        public string client_id;
        public User(string email, string password)
        {
            connection = "Username-Password-Authentication";
            client_id = "40aKnZOwwEdH5XCnhUqz5spbl1RgJ5rv";
            this.email = email;
            this.password = password;
        }
	}


    string baseURL = @"https://allappsever.auth0.com/";
    string signupURL = @"dbconnections/signup";
    string loginURL = @"oauth/token";
    string defaultApiEndpoint = "api/v2/";
    string userInfo = "userinfo";

    void Start()
    {
        User user = new User("djy118@gmail.com", "dadadada");
        StartCoroutine(Read(user));
    }

    IEnumerator Create(User user)
    {
        WWWForm form = new WWWForm();
        form.AddField("connection", user.connection);
        form.AddField("client_id", user.client_id);
        form.AddField("email", user.email);
        form.AddField("password", user.password);
        UnityWebRequest www = UnityWebRequest.Post(baseURL + signupURL, form);
        
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Create request complete");
        }
    }

    IEnumerator Read(string accessToken)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Get(baseURL + userInfo);

        www.SetRequestHeader("Authorization", "Bearer " + accessToken);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Read request complete");
        }
    }

    IEnumerator Authenticate(User user)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "password");
        form.AddField("audience", baseURL + defaultApiEndpoint);
        form.AddField("client_id", user.client_id);
        form.AddField("username", user.email);
        form.AddField("password", user.password);
        UnityWebRequest www = UnityWebRequest.Post(baseURL + loginURL, form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Read request complete");
        }
    }

    void Update()
    {
    }
}
