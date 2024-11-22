using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;
    private GameManager gm;

    public bool isReal;
    public string objectName;

    public bool hasFakeKey;
    // Start is called before the first frame update
    void Awake()
    {
        gm = GameManager.instance;;
        PlayerController = FindObjectOfType<PlayerController>();
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectName == "Key")
        {
            if (isReal)
            {
                if (PlayerController.hasKey)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                if (hasFakeKey)
                {
                    this.gameObject.SetActive(false);
                }
            }


            gameObject.GetComponent<Outline>().enabled = PlayerController.looksKey;
        }

        if (objectName == "hammer")
        {
            if (gm.hasHammer)
            {
                this.gameObject.SetActive(false);
            }
            gameObject.GetComponent<Outline>().enabled = PlayerController.looksHammer;
        }

        if (objectName == "Glasses")
        {
            if (gm.hasGlasses)
            {
                this.gameObject.SetActive(false);
            }
            gameObject.GetComponent<Outline>().enabled = PlayerController.looksGlasses;
        }

        if (objectName == "jumpBoost")
        {
            if (gm.hasJump)
            {
                this.gameObject.SetActive(false);
            }
        }

    }
}
