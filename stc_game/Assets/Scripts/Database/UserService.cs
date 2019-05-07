using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserService
{
    public static UserService Instance { get; } = new UserService();
    public User user { get; private set; } = new User();
    private UserApi userApi = UserApi.Instance;
    public event Action LoadUserCallback;

    // This class is to fix Unity's JSON serialization problem
    private class JsonUserWrapper
    {
        public List<User> users = new List<User>();
    }

    private UserService()
    {
        userApi.ReadUserCallback += HandleReadUserCallback;
        if (user == null)
        {
            user = new User();
        }
    }

    public void CreateUser()
    {
        user = new User(AuthService.Instance.authUser.nickname, AuthService.Instance.authUser.sub);
        userApi.CreateOrUpdate(user);
    }

    public void SaveUser()
    {
        userApi.CreateOrUpdate(user);
    }

    public void GetUser(string authUserId)
    {
        userApi.Read(authUserId);
    }

    // This is the function that loads the User from restdb.io into the game
    public void LoadUserData(UnityWebRequest www)
    {
        // Because FromJson is stupid and ridiculous and old and non-modern POS arrays are not supported as unity objs
        string jsonUser = www.downloadHandler.text;

        // If user data exists in database, extract the user
        if (jsonUser != "")
        {
            try
            {
                var userList = new JsonUserWrapper();
                jsonUser = jsonUser.Replace("\n", "");
                jsonUser = "{ \"users\": " + jsonUser + "}";
                userList = JsonUtility.FromJson<JsonUserWrapper>(jsonUser);
                if (userList.users.Count > 0)
                {
                    this.user = userList.users[0];
                }
                // If there is no user, it is the first login so create one
                if (this.user == null || !this.user.Exists())
                {
                    CreateUser();
                }
                else
                {
                    LoadUserCallback.Invoke();
                }
            }
            catch (Exception e)
            {
                Debug.Log("(UserService) Error: " + e.ToString());
            }
            
        }
        else
        {
            Debug.Log("Load User failed completed.");
        }
    }

    public void HandleReadUserCallback(AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("UserService Error: Database error" + www.responseCode);
            if (www.responseCode.Equals(500))
            {
                Debug.Log("500 Error: not specified");
            }
            else
            {
                Debug.Log("Database read or write error");
            }
        }
        // If was Get request and was successful
        else if (www.responseCode.Equals(200))
        {
            Debug.Log("On HTTP GET Request");
            LoadUserData(www);
        }
        // If was Post request and was successful
        else if (www.responseCode.Equals(201))
        {
            Debug.Log("On HTTP POST Request");
            LoadUserData(www);
        }
    }
}

