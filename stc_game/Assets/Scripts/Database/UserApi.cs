using DatabaseEnums;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;

public class UserApi
{
    public static UserApi Instance { get; } = new UserApi();
    static DatabaseCollections playerCollection = DatabaseCollections.playerstest;
    public event Action<AsyncOperation> ReadCallback;

    public void CreateOrUpdate(User user)
    {
        if(user._id == "")
        {
            BaseApi.Post(playerCollection, new NewUser(user.CharacterName, user.UserAuthenticationId)).completed += ReadCallback; ;
        }
        else
        {
            BaseApi.Put(playerCollection, user, user._id);
        }
    }

    public void Read(string userId)
    {
        BaseApi.Get(playerCollection, userId).completed += ReadCallback;
    }

    public void Delete(User user)
    {
        BaseApi.Delete(playerCollection, user._id);
    }


}