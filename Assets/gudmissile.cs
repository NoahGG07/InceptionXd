using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gudmissile : MonoBehaviour
{
    private Transform playerPos;
    public float speed;

    public float explosionRange;
    private Rigidbody rb;

    public GameObject explosionEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerPos = FindObjectOfType<Boss>().transform;
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
        if (playerPos != null)
        {
            gameObject.transform.LookAt(playerPos);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        Explode();
    }

    void Explode()
    {
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

            ObjectHealth pcc = collider.GetComponent<ObjectHealth>();

            if (pcc != null)
            {
                pcc.objectHealth -= 20;
            }
            
            ObjectHealth oh = collider.GetComponent<ObjectHealth>();

            if (oh != null)
            {
                oh.objectHealth -= 5;
            }

        }
        Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity); 
        Destroy(this.gameObject, 0.2f);
        
    }
}
