using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    private CanvasManager cm;

    private void Start()
    {
        cm = FindObjectOfType<CanvasManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //die u piece of trash
            cm.Die();
        }
    }
}
