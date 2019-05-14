using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutCanvas : MonoBehaviour
{
    public Canvas logoutCanvas;
    // Start is called before the first frame update
    void Start()
    {
        logoutCanvas = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
