using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


public class FormManager : MonoBehaviour {

	public InputField emailInput;
	public InputField passwordInput;

	public Button signUpButton;
	public Button loginButton;
	private AuthManager authManager;
	
	void Awake() {
		ToggleButtonStates(false);

		//Auth delegate subcriptions
		//authManager.authCallback += HandleAuthCallback;
	}

	public void ValidateEmail() {
		/*string email = emailInput.text;
		var regexPattern = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$@";
		if (email != "" && Regex.IsMatch(email, regexPattern) {
			ToggleButtonStates(true);
		} else {
			ToggleButtonStates (false);
		}*/
	}

	public void OnSignUp() {
		//authManager.SignUpNewUserWithEmailAndPassword(emailInput.text, passwordInput.text);
		//Debug.Log("Sign Up");
	}

	public void OnLogin() {
		//authManager.LoginExistingUser(emailInput.text, passwordInput.text);
		//Debug.Log("Login");
	}

	//IEnumerator HandleAuthCallback (Task<Firebase.Auth.FirebaseUser> task, string operation) {
		/*if (task.IsFaulted || task.IsCanceled) {
			Debug.LogError("Sorry, an error happened creating your account, ERROR: " + task.Exception);
		} else if (task.IsCompleted) {
			Firebase.Auth.FirebaseUser newPlayer = task.Result;
			Debug.LogFormat("Welcome! {0}", newPlayer.Email);
			yield return true;
		}*/
	//}

	/*void onDestroy() {
		authManager.authCallback -= HandleAuthCallback;
	}*/

	private void ToggleButtonStates(bool toState) {
		signUpButton.interactable = toState;
		loginButton.interactable = toState;
	}
}
