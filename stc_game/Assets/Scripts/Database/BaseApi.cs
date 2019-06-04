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
        string query = databaseURI + collection.ToString() + "?q={\"UserAuthenticationId\": \"" + userAuthId + "\"}".Replace(@"\","");
        UnityWebRequest www = UnityWebRequest.Get(query);
        addHeaders(www);
        return www.SendWebRequest();
    }

    public static UnityWebRequestAsyncOperation Put<T>(DatabaseCollections collection, T data, string objectId)
    {
        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(data));
        UnityWebRequest www = UnityWebRequest.Put(databaseURI + collection.ToString() + "/" + objectId, "PUT");
        www.uploadHandler = new UploadHandlerRaw(bytes);
        www.uploadHandler.contentType = "application/json";
        addHeaders(www);
        return www.SendWebRequest();
    }

    public static UnityWebRequestAsyncOperation Post<T>(DatabaseCollections collection, T data)
    {
        byte[] bytes = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(data));

        UnityWebRequest www = UnityWebRequest.Post(databaseURI + collection.ToString(), "POST");
        www.uploadHandler = new UploadHandlerRaw(bytes);
        www.uploadHandler.contentType = "application/json";
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
        webRequest.SetRequestHeader("Content-Type", "application/json");
    }


}