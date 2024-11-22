using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;

public class Missile : MonoBehaviour
{
    private Transform playerPos;
    public float speed;

    public float explosionRange;
    private Rigidbody rb;

    public GameObject explosionEffect;

    private AudioSource explosionsfx;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerPos = FindObjectOfType<PlayerController>().transform;
        explosionsfx = gameObject.GetComponent<AudioSource>();
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
        gameObject.transform.LookAt(playerPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        explosionsfx.Play();
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        
         
        foreach (var collider in colliders)
        {
          //  if (collider.transform.GetComponent<PlayerController>() != null)
        //    {
               // collider.transform.GetComponent<PlayerController>().Die();
         //   }

            Rigidbody rgb = collider.GetComponent<Rigidbody>(); 
            
            if (rgb != null)
            {
                rgb.AddExplosionForce(1300, transform.position, explosionRange, 300);
            }

            PlayerController pcc = collider.GetComponent<PlayerController>();

            if (pcc != null)
            {
                pcc.currentHealth -= 20;
            }
            
            ObjectHealth oh = collider.GetComponent<ObjectHealth>();

            if (oh != null)
            {
                oh.objectHealth -= 5;
            }

        }
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity); 
        Destroy(this.gameObject, 0.4f);
        
    }
}
