﻿
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class FormManager : MonoBehaviour {

    private AuthService authService = AuthService.Instance;

    public InputField emailInput;
	public InputField passwordInput;
    public Text statusText;

	public Button signUpButton;
	public Button loginButton;

    private bool emailValid = false;
    private bool passwordValid = false;

    void Awake()
    {
        // registering callback for sign-up and login callbacks
        authService.AuthCallback += HandleAuthCallback;

        // registering button-click and other triggered events
        signUpButton.onClick.AddListener(OnSignUp);
        loginButton.onClick.AddListener(OnLogin);
        emailInput.onValueChanged.AddListener(ValidateEmail);
        passwordInput.onValueChanged.AddListener(ValidatePassword);

        // disable form buttons until user inputs a valid email
        ToggleButtonStates(false);
	}

    private void ValidateEmail(string email) {
		var regexPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
		if (email != "" && Regex.IsMatch(email, regexPattern)) {
            emailValid = true;
		} else {
            emailValid = false;
		}
        SetButtonStates();
	}

    private void ValidatePassword(string password)
    {
        if (password.Length > 7) {
            passwordValid = true;
        } else {
            passwordValid = false;
        }
        SetButtonStates();
    }

	public void OnSignUp() {
		authService.SignUpNewUserWithEmailAndPassword(emailInput.text, passwordInput.text);
	}

	public void OnLogin() {
		authService.LoginExistingUser(emailInput.text, passwordInput.text);
        
	}

	void HandleAuthCallback (AsyncOperation res) {
        UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = res as UnityWebRequestAsyncOperation;
        UnityWebRequest www = unityWebRequestAsyncOperation.webRequest;

        if (www.isNetworkError || www.isHttpError)
        {
            if (www.responseCode.Equals(403) || www.responseCode.Equals(401))
            {
                statusText.fontSize = 14;
                statusText.text = "Incorrect email or password";
            }
            else if (www.responseCode.Equals(400))
            {
                statusText.fontSize = 14;
                statusText.text = "An account with this email already exists";
            }
            else if (www.responseCode.Equals(429))
            {
                statusText.fontSize = 14;
                statusText.text = "Too many login attempts: Please try again later";
            }
            else
            {
                statusText.text = "You Fail";
                statusText.fontSize = 80;
                Debug.Log(www.error + " " + www.responseCode);
            }
        }
        
        else if (www.responseCode.Equals(200))
        {
            SceneManager.LoadScene("Temp Scene");
        }
    }

	void onDestroy() {
		authService.AuthCallback -= HandleAuthCallback;
	}

    private void SetButtonStates()
    {
        if (emailValid && passwordValid)
        {
            ToggleButtonStates(true);
        }
        else
        {
            ToggleButtonStates(false);
        }
    }

	private void ToggleButtonStates(bool toState) {
		signUpButton.interactable = toState;
		loginButton.interactable = toState;
	}
}
