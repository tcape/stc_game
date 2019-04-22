using UnityEngine.Networking;
using UnityEngine;
using System.Text.RegularExpressions;
using DatabaseEnums;

public static class BaseApi
{
    const string databaseURI = @"https://stcgame-36c2.restdb.io/rest/";
    const string apiKey = @"2142d9fff244df7b8ff4aa6d943c2b9e89433";
    public static UnityWebRequestAsyncOperation Get(DatabaseCollections collection, string userAuthId)
    {
        if(userAuthId == null || userAuthId == "")
        {
            throw new System.Exception("No user authentication was supplied");
        }
        UnityWebRequest www = UnityWebRequest.Get(databaseURI + collection.ToString() + "?={'userAuthId': '" + userAuthId + "'}");
        addHeaders(www);

        return www.SendWebRequest();
    }

    public static UnityWebRequestAsyncOperation Put<T>(DatabaseCollections collection, T data, string objectId)
    {
        UnityWebRequest www = UnityWebRequest.Put(databaseURI + collection.ToString() + "/" + objectId, JsonUtility.ToJson(data));
        addHeaders(www);


        return www.SendWebRequest();
    }

    public static UnityWebRequestAsyncOperation Post<T>(DatabaseCollections collection, T data)
    {
        string lmao = Regex.Unescape(JsonUtility.ToJson(data));
        UnityWebRequest www = UnityWebRequest.Post(databaseURI + collection.ToString(), JsonUtility.ToJson(data));
        addHeaders(www);
        return www.SendWebRequest();
    }

    public static UnityWebRequestAsyncOperation Delete(DatabaseCollections collection, string objectId)
    {
        UnityWebRequest www = UnityWebRequest.Delete(databaseURI + collection.ToString() + "/" + objectId);
        addHeaders(www);
        return www.SendWebRequest();
    }


    private static void addHeaders(UnityWebRequest webRequest)
    {
        webRequest.SetRequestHeader("cache-control", "no-cache");
        webRequest.SetRequestHeader("x-apikey", apiKey);
        webRequest.SetRequestHeader("content-type", "application/json");
    }


}