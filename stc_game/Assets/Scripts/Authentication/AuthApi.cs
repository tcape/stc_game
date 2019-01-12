using UnityEngine.Networking;
using UnityEngine;

public class AuthApi {

    string userConnection = "Username-Password-Authentication";
    string userClientId = "40aKnZOwwEdH5XCnhUqz5spbl1RgJ5rv";
    string baseURL = @"https://allappsever.auth0.com/";
    string signupURL = @"dbconnections/signup";
    string loginURL = @"oauth/token";
    string defaultApiEndpoint = "api/v2/";
    string userInfo = "userinfo";

    private AuthApi() {}

    public static AuthApi Instance { get; } = new AuthApi();

    public UnityWebRequestAsyncOperation Create(AuthReq request)
    {
        WWWForm form = new WWWForm();
        form.AddField("connection", userConnection);
        form.AddField("client_id", userClientId);
        form.AddField("email", request.email);
        form.AddField("password", request.password);
        UnityWebRequest www = UnityWebRequest.Post(baseURL + signupURL, form);
        
        return www.SendWebRequest();
    }

    public UnityWebRequestAsyncOperation Read(string accessToken)
    {
        UnityWebRequest www = UnityWebRequest.Get(baseURL + userInfo);

        www.SetRequestHeader("Authorization", "Bearer " + accessToken);
        return www.SendWebRequest();
    }

    public UnityWebRequestAsyncOperation Authenticate(AuthReq request)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "password");
        form.AddField("audience", baseURL + defaultApiEndpoint);
        form.AddField("client_id", userClientId);
        form.AddField("username", request.email);
        form.AddField("password", request.password);
        form.AddField("scope", "openid");

        UnityWebRequest www = UnityWebRequest.Post(baseURL + loginURL, form);

        return www.SendWebRequest();
    }
}
