using Assets.Scripts.CharacterBehavior.Drops;
using Kryz.CharacterStats.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestOpener : MonoBehaviour
{
    private Camera cam;
    private Animator animator;
    public bool opened;
    private GameObject player;
    private HeroClass playerClass;
    public float range;
    public double goldAmount;
    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerClass = player.GetComponent<Hero>().heroClass;
        animator = GetComponent<Animator>();
        animator.SetBool("open", false);
        if (opened)
        {
            GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OpenAndCloseChest();
        }
    }

    public void SetOpened(bool o)
    {
        opened = o;
    }

    private void OpenAndCloseChest()
    {
        var distance = Math.Abs(Vector3.Distance(transform.position, player.transform.position));

        if (distance > range)
            return;

        // raycast at mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // if hit something get the hit
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == gameObject.GetComponent<BoxCollider>())
            {
                if (!animator.GetBool("open"))
                {
                    animator.SetBool("open", true);
                    if (!opened)
                    {
                        // instatiate stuff
                        StartCoroutine(Spawn());
                        GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
                        opened = true;
                    }
                }
                else if (animator.GetBool("open"))
                {
                    animator.SetBool("open", false);
                }
            }
        }
    }
    
    private IEnumerator SpawnGold()
    {
        if (goldAmount > 0)
        {
            yield return new WaitForSeconds(1f);
            var gold = Instantiate(Resources.Load("Prefabs/Gold1"), gameObject.transform) as GameObject;
            gold.transform.rotation = transform.rotation;
            gold.transform.position = transform.position + new Vector3(1f, 0.1f, 0f); // need to get this to make gold appear in front of chest everytime
            gold.GetComponent<Gold>().SetAmount(goldAmount);
        }
    }

    private IEnumerator SpawnItems()
    {
        float rotation = (float)(Math.PI * gameObject.transform.rotation.eulerAngles.y / 180.0f) - (float)Math.PI / 4f;
        foreach (var item in items)
        {
            if (item.GetComponent<GameItem>().item.itemClass == ItemClass.Any || 
                item.GetComponent<GameItem>().item.itemClass.ToString() == playerClass.ToString())
            {
                yield return new WaitForSeconds(0.2f);
                float x = (float)Math.Sin(rotation) * 2f;
                float z = (float)Math.Cos(rotation) * 2f;
                var droppedItem = Instantiate(item, gameObject.transform);
                droppedItem.transform.position = transform.position + new Vector3(x, 0.7f, z);
                droppedItem.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 90);
                rotation += (float)(Math.PI / 4f);
            }
        }
    }

    private IEnumerator Spawn()
    {
        yield return SpawnGold();
        yield return SpawnItems();
    }
}
