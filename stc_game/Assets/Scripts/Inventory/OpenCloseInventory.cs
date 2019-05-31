using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInventory : MonoBehaviour
{
    public Canvas characterInventory;

    private void Awake()
    {
        characterInventory = GameObject.Find("Character Inventory").GetComponent<Canvas>();
        characterInventory.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (characterInventory.enabled)
        {
            characterInventory.enabled = false;
        }
        else
        {
            characterInventory.enabled = true;
        }
    }
}
