using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Transform playerPos;
    public GameObject explosionEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(this, 1f);
    }

    private void Awake()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
        gameObject.transform.LookAt(playerPos);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<PlayerController>() != null)
        {
            PlayerController pc = other.transform.GetComponent<PlayerController>();

            pc.currentHealth -= 1;
        }
        if (other.transform.GetComponent<ObjectHealth>() != null)
        {
            ObjectHealth pc = other.transform.GetComponent<ObjectHealth>();

            pc.objectHealth -= 1;
        }
        Destroy(this, 0.01f);
    }

   
}
