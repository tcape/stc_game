using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class AuthService {

    public static AuthService Instance { get; } = new AuthService();

    // instantiating public variables
    private AuthApi auth = AuthApi.Instance;
    public event Action<AsyncOperation> AuthCallback;

    private AuthService() {
    }

    public void SignUpNewUserWithEmailAndPassword(string email, string password) {

        User user = new User(email, password);
        auth.Create(user).completed += AuthCallback;
    }

    public void LoginExistingUser(string email, string password) {

        User user = new User(email, password);
        auth.Authenticate(user).completed += AuthCallback;
    }
}
