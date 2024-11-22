using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == playerController.gameObject)
            return;
        
        playerController.SetGroundedState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == playerController.gameObject)
            return;
        
        playerController.SetGroundedState(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == playerController.gameObject)
            return;
        
        playerController.SetGroundedState(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerController.gameObject)
            return;
        
        playerController.SetGroundedState(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == playerController.gameObject)
            return;
        
        playerController.SetGroundedState(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == playerController.gameObject)
            return;
        
        playerController.SetGroundedState(true);
    }
}
