using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameManager gm;
    
    public GameObject missile;
    public ParticleSystem particles;
    public Transform tip;
    public Transform playerPos;
    public GameObject turretHead;
    public float range;
    public float fireRate;
    public float firerateNazi;
    private float NextTimeToFire;
    public GameObject parent;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        turretHead.transform.LookAt(playerPos.position);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider nearObjects in colliders) 
        {
            if (nearObjects.CompareTag("Player") && Time.time >= NextTimeToFire)
            {
                if (gm.difficulty == 1)
                {
                    NextTimeToFire = Time.time + 1f / fireRate;
                }else if (gm.difficulty == 2)
                {
                    NextTimeToFire = Time.time + 1f / fireRate;
                }else if (gm.difficulty == 3)
                {
                    NextTimeToFire = Time.time + 1f / firerateNazi;
                }
                Shoot();
                
            }
        }
    }

    void Shoot()
    {
        particles.Play();
        var position = tip.position;
        (Instantiate(missile, position, Quaternion.identity) as GameObject).transform.parent = parent.transform;
    }

  
}
