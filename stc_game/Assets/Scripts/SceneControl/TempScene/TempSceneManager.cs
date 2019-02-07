
using UnityEngine;
using UnityEngine.UI;

public class TempSceneManager : MonoBehaviour {

    public Text sceneUser;

	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
        if (AuthService.Instance.authUser != null)
        {
            sceneUser.text = AuthService.Instance.authUser.nickname + "!";
        }
    }
}
