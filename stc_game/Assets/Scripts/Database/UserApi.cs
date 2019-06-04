using DatabaseEnums;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;

public class UserApi
{
    public static UserApi Instance { get; } = new UserApi();
    static DatabaseCollections playerCollection = DatabaseCollections.players;
    public event Action<AsyncOperation> ReadUserCallback;

    public void CreateOrUpdate(User user)
    {
        if(user._id == "")
        {
            BaseApi.Post(playerCollection, new BaseUser(user.Name, user.UserAuthenticationId)).completed += ReadUserCallback;
        }
        else
        {
            BaseApi.Put(playerCollection, user, user._id);
        }
    }

    public void Read(string userId)
    {
        BaseApi.Get(playerCollection, userId).completed += ReadUserCallback;
    }

    public void Delete(User user)
    {
        BaseApi.Delete(playerCollection, user._id);
    }


}