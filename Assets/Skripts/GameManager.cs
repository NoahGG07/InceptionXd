using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] private PlayerController pc;
    public bool hasHammer;
    public bool hasGlasses;
    public bool hasJump;


    public int difficulty;


    void Awake()
    {
        
        Time.timeScale = 1;
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;



        if (sceneNumber == 0)
        {
            hasHammer = false;
            hasGlasses = false;
            hasJump = false;

        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }




}