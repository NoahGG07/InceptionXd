using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public PlayerController pc;


    [SerializeField] public bool IsLastScene;

    public bool isFirstLevel;
    [SerializeField] private GameObject Message;

    public Transform rsp;
    
    private void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (pc.hasKey)
        {
            if (IsLastScene)
            {
                SceneManager.LoadScene("MAinMenu");
            }else if (!IsLastScene)
            {
                if (isFirstLevel)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }

                
            }

            
        }


    }
}
