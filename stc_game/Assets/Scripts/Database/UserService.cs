using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserService
{
    public static UserService Instance { get; } = new UserService();
    public User User { get; private set; } = new User();
    private UserApi userApi = UserApi.Instance;
    public event Action LoadUserCallback;

    private UserService()
    {
        userApi.ReadCallback += HandleReadCallback;
        if (User == null)
        {
            User = new User();
        }
    }

    public void CreateUser()
    {
        User.UserAuthenticationId = AuthService.Instance.authUser.sub;
        User.CharacterName = AuthService.Instance.authUser.nickname;
        userApi.CreateOrUpdate(User);
    }

    public void SaveUser()
    {
        userApi.CreateOrUpdate(User);
    }

    public void GetUser(string authUserId)
    {
        userApi.Read(authUserId);
    }

    public void HandleReadCallback(AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;
        // Because FromJson is stupid and ridiculous and old and non-modern POS arrays are not supported as unity objs
        string value = www.downloadHandler.text.TrimStart('[').TrimEnd(']').Replace("\n", "");
        if (value != "")
        {
            User = JsonUtility.FromJson<User>(value);
        }
        LoadUserCallback.Invoke();
    }
}

