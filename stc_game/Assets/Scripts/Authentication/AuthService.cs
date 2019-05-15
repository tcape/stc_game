using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Networking;

public class AuthService {

    public static AuthService Instance { get; } = new AuthService();

    // instantiating public variables
    private AuthApi auth = AuthApi.Instance;
    public AuthUser authUser { get; private set; }
    private AuthReq authRequest; 
    public event Action<AsyncOperation> AuthenticationCallback;
    public event Action UserIsLoggedIn;

    private AuthService() {
    }

    // creates a http request with user credentials and attempts to create a new user
    public void SignUpNewUserWithEmailAndPassword(string email, string password) {

        authRequest = new AuthReq(email, password);
        auth.Create(authRequest).completed += SignUpCallback;
    }

    // creates an http request with user credentials and attempts to authenticate the user
    public void LoginUserWithEmailAndPassword(string email, string password) {

        authRequest = new AuthReq(email, password);
        auth.Authenticate(authRequest).completed += AuthenticationCallback;
    }

    public void LoginExistingUser()
    {
        AuthRes res = new AuthRes();
        res.access_token = PlayerPrefs.GetString(GameStrings.LocalStorage.AuthToken);
        GetAuthUser(res);
    }

    public bool isLoggedIn()
    {
        return PlayerPrefs.GetString(GameStrings.LocalStorage.AuthToken) != "";
    }

    public void Logout()
    {
        PlayerPrefs.DeleteKey(GameStrings.LocalStorage.AuthToken);
    }

    // take an authentication result and request user data then call event to store user data
    public void GetAuthUser(AuthRes authRes)
    {
        PlayerPrefs.SetString(GameStrings.LocalStorage.AuthToken, authRes.access_token);
        auth.Read(authRes.access_token).completed += GetAuthUserCallback;
    }

    // takes a authentication user data result and sets the auth user
    private void GetAuthUserCallback(AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        if (www.responseCode.Equals(200))
        {
            authUser = JsonUtility.FromJson<AuthUser>(www.downloadHandler.text);
            UserIsLoggedIn.Invoke();
        }
        else
        {
            Debug.Log("Auth result was not able to get auth user data");
        }
    }

    // upon signing up, request authentication on the result, otherwise notify game scene to handle the signup result
    private void SignUpCallback (AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        // if success, 
        if (www.responseCode.Equals(200))
        {
            auth.Authenticate(authRequest).completed += AuthenticationCallback;
        }
        else
        {
            AuthenticationCallback(res);
        }
    }
}
