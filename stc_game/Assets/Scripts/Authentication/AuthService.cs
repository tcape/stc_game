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
    public event Action<AsyncOperation> LoginUICallback;

    private AuthService() {
    }

    // creates a http request with user credentials and attempts to create a new user
    public void SignUpNewUserWithEmailAndPassword(string email, string password) {

        authRequest = new AuthReq(email, password);
        auth.Create(authRequest).completed += SignUpCallback;
    }

    // creates an http request with user credentials and attempts to authenticate the user
    public void LoginExistingUser(string email, string password) {

        authRequest = new AuthReq(email, password);
        auth.Authenticate(authRequest).completed += AuthCallback;
    }

    // take an authentication result and request user data then call event to store user data
    public void GetUserData(AuthRes authRes)
    {
        auth.Read(authRes.access_token).completed += ReadUserCallback;
    }

    // upon authenticating, notify the game scene to handle the authentication result
    private void AuthCallback (AsyncOperation res)
    {
        LoginUICallback(res);
    }

    // takes a user data result and sets the user
    private void ReadUserCallback(AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        if (www.responseCode.Equals(200))
        {
            authUser = JsonUtility.FromJson<AuthUser>(www.downloadHandler.text);
        }
    }

    // upon signing up, request authentication on the result, otherwise notify game scene to handle the signup result
    private void SignUpCallback (AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        if (www.responseCode.Equals(200))
        {
            auth.Authenticate(authRequest).completed += LoginUICallback;
        }
        else
        {
            LoginUICallback(res);
        }
    }
}
