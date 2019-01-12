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
    public event Action<AsyncOperation> AuthCallback;

    private AuthService() {
    }

    public void SignUpNewUserWithEmailAndPassword(string email, string password) {

        authRequest = new AuthReq(email, password);
        auth.Create(authRequest).completed += SignUpCallback;
    }

    public void LoginExistingUser(string email, string password) {

        authRequest = new AuthReq(email, password);
        auth.Authenticate(authRequest).completed += AuthCallback;
    }

    public void GetUserData(AuthRes authRes)
    {
        auth.Read(authRes.access_token).completed += ReadUserCallback; ;
    }

    private void ReadUserCallback(AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        if (www.responseCode.Equals(200))
        {
            authUser = JsonUtility.FromJson<AuthUser>(www.downloadHandler.text);
        }
    }

    private void SignUpCallback (AsyncOperation res)
    {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        if (www.responseCode.Equals(200))
        {
            auth.Authenticate(authRequest).completed += AuthCallback;
        }
        else
        {
            Debug.Log("There wan an error creating the account");
        }
    }
}
