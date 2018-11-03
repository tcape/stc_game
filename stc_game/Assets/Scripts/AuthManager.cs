using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour {

    //Firebase API variables
    Firebase.Auth.FirebaseAuth auth;

    public delegate IEnumerator AuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation);
    public event AuthCallback authCallback;
    void Awake() {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SignUpNewUserWithEmailAndPassword(string email, string password) {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            StartCoroutine(authCallback(task, "sign_up"));
        });
    }

    public void LoginExistingUser(string email, string password) {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith (task => {
            StartCoroutine(authCallback(task, "login"));
        });
    }
}
