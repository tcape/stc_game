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

    private void Start()
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

    private void SetMaterialToTransparentFade(Material material)
    {
        material.SetFloat("_Mode", 2);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }

    private void SetMaterialToOpaque(Material material)
    {
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        material.SetInt("_ZWrite", 1);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = -1;
    }

    private IEnumerator FadeCharacter(Material material, float lerpDuration)
    {
        SetMaterialToTransparentFade(material);
        var meshColor = material.color;
        Color startLerp = new Color(meshColor.r, meshColor.g, meshColor.b, 0f);
        Color targetLerp = new Color(meshColor.r, meshColor.g, meshColor.b, 1f);
        material.color = startLerp;
        activeCharacter.SetActive(true);
        float lerpStart_Time = Time.time;
        float lerpProgress;
        bool lerping = true;
        while (lerping)
        {
            yield return new WaitForEndOfFrame();
            lerpProgress = Time.time - lerpStart_Time;
            if (material != null)
            {
                material.color = Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
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
      
        SetMaterialToOpaque(material);
        yield break;
    }

    private void OnClickMageButton()
    {
        if (activeCharacter == warrior)
        {
            activeCharacter.SetActive(false);
            activeCharacter = mage;
            var teleportEffect = Instantiate(Resources.Load("FX/Mage_Entrance_Effect_2")) as GameObject;
            var poofEffect = Instantiate(Resources.Load("FX/Mage_Entrance_Burst")) as GameObject;
            teleportEffect.transform.position = mage.transform.position + new Vector3(-0.3f, 1.7f, 0f);
            poofEffect.transform.position = mage.transform.position + new Vector3(0f, 1.4f, 0f);
            StartCoroutine(FadeCharacter(mage.GetComponentInChildren<SkinnedMeshRenderer>().material, 0.6f));
            MageAnimation();
        }
    }

    private void OnClickWarriorButton()
    {
        if (activeCharacter == mage)
        {
            activeCharacter.SetActive(false);
            activeCharacter = warrior;
            var warriorEffectBurst = Instantiate(Resources.Load("FX/Warrior_Entrance_Burst")) as GameObject;
            var warriorEntrance = Instantiate(Resources.Load("FX/Warrior_Entrance_Effect")) as GameObject;
            warriorEffectBurst.transform.position = warrior.transform.position + new Vector3(0f, 1.2f, 0f);
            warriorEntrance.transform.position = warrior.transform.position + new Vector3(-0.3f, 1.2f, 0f);
            StartCoroutine(FadeCharacter(warrior.GetComponentInChildren<SkinnedMeshRenderer>().material, 0.6f));
            StartCoroutine(WarriorAttacks());
        }
    }

    private IEnumerator WarriorAttacks()
    {
        var animator = warrior.GetComponent<Animator>();
        animator.SetBool("Attacking", true);
        animator.SetBool("Moving", false);
        animator.SetInteger("Attack", 1);
        yield return new WaitForSeconds(.2f);
        animator.SetInteger("Attack", 2);
        yield return new WaitForSeconds(.5f);
        animator.SetInteger("Attack", 0);
        animator.SetBool("Attacking", false);
    }

    private void MageAnimation()
    {
        var animator = mage.GetComponent<Animator>();
        animator.SetTrigger("MoveAttack1");
    }
}
