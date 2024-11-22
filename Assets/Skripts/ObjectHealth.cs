using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectHealth : MonoBehaviour
{

    public GameObject destroyEffect;
    public Transform pos;
    public int objectHealth;
    public int maxHealth = 100;
    public int maxHealth2 = 200;
    public int maxHealth3 = 500;
    public bool hasText;
    public TMP_Text text;
    public bool doesDrop;

    private bool hasDestroyed;

    private GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        
        if (gm.difficulty == 1)
        {
            objectHealth = maxHealth;
        }else if (gm.difficulty == 2)
        {
            objectHealth = maxHealth2;
        }else if (gm.difficulty == 3)
        {
            objectHealth = maxHealth3;
        }

        hasDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = objectHealth.ToString();
        
        if (objectHealth <= 0 && !hasDestroyed)
        {
            DestroyObject();
        }
        
    }

    public GameObject key;
    void DestroyObject()
    {
        Instantiate(destroyEffect, pos.position, Quaternion.identity);
        if (doesDrop)
        {
            Instantiate(key, pos.position, Quaternion.identity);
        }

        hasDestroyed = true;
        Destroy(this.gameObject, 0.2f);
    }
}
