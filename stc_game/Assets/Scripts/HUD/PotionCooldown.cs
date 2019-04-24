using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionCooldown : MonoBehaviour
{
    [SerializeField] public Transform parent;
    private float cooldown;
    public Image image;
    public float startTime;

    private void Awake()
    {
        cooldown = parent.GetComponent<PotionButton>().cooldown;
        image = GetComponent<Image>();
        image.fillAmount = 0;
    }
    
    private void Update()
    {
        if (startTime < Time.time)
        {
            image.fillAmount = 1f - (Time.time - startTime) / cooldown;
        }
    }

    public void StartCooldown()
    {
        startTime = Time.time;
    }

}
