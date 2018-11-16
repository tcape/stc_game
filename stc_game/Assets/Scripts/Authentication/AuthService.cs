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
    public event Action<AsyncOperation> AuthCallback;

    private AuthService() {
    }

    public void SignUpNewUserWithEmailAndPassword(string email, string password) {

        AuthReq request = new AuthReq(email, password);
        auth.Create(request).completed += AuthCallback;
    }

    public void LoginExistingUser(string email, string password) {

        AuthReq request = new AuthReq(email, password);
        auth.Authenticate(request).completed += AuthCallback;
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
}
