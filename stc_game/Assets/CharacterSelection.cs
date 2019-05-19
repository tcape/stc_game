using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public Button mageButton;
    public Button warriorButton;
    public GameObject mage;
    public GameObject warrior;
    private GameObject activeCharacter;

    // Start is called before the first frame update
    void Start()
    {
        mageButton.onClick.AddListener(OnClickMageButton);
        warriorButton.onClick.AddListener(OnClickWarriorButton);
        warrior = GameObject.Find("WarriorPrefab");
        mage = GameObject.Find("MagePrefab");
        warrior.SetActive(false);
        mage.SetActive(false);
        activeCharacter = warrior;
        activeCharacter.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeMage(MeshRenderer meshRender, float lerpDuration)
    {
        var meshColor = meshRender.material.color;
        Color startLerp = new Color(meshColor.r, meshColor.g, meshColor.b, 0f);
        Color targetLerp = new Color(meshColor.r, meshColor.g, meshColor.b, 1f);
        meshRender.material.color = startLerp;
        activeCharacter.SetActive(true);
        float lerpStart_Time = Time.time;
        float lerpProgress;
        bool lerping = true;
        while (lerping)
        {
            yield return new WaitForEndOfFrame();
            lerpProgress = Time.time - lerpStart_Time;
            if (meshRender != null)
            {
                meshRender.material.color = Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
            }
            else
            {
                lerping = false;
            }
            if (lerpProgress >= lerpDuration)
            {
                lerping = false;
            }
        }
        yield break;
    }

    void OnClickMageButton()
    {
        if (activeCharacter == warrior)
        {
            activeCharacter.SetActive(false);
            activeCharacter = mage;
            var teleportEffect = Instantiate(Resources.Load("FX/Mage_Entrance_Effect_2")) as GameObject;
            var poofEffect = Instantiate(Resources.Load("FX/Mage_Entrance_Burst")) as GameObject;
            teleportEffect.transform.position = mage.transform.position + new Vector3(.5f, 1.8f, 0f);
            poofEffect.transform.position = mage.transform.position + new Vector3(.1f, 1f, 0f);
            StartCoroutine(FadeMage(mage.GetComponent<MeshRenderer>(), 2f));
        }
    }

    void OnClickWarriorButton()
    {
        if (activeCharacter == mage)
        {
            activeCharacter.SetActive(false);
            activeCharacter = warrior;
            activeCharacter.SetActive(true);
        }
    }
}
