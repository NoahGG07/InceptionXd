using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gudturret : MonoBehaviour
{
    private GameManager gm;
    
    public GameObject missile;
    public ParticleSystem particles;
    public Transform tip;
    public Transform bossPos;
    public GameObject turretHead;
    public float range;
    public float fireRate;
    public float firerateNazi;
    private float NextTimeToFire;
    public GameObject parent;
    public int missileAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossPos != null)
        {
            turretHead.transform.LookAt(bossPos.position);
        }

        
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider nearObjects in colliders) 
        {
            if (nearObjects.CompareTag("Player") && Time.time >= NextTimeToFire && missileAmount >= 0)
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
        missileAmount -= 1;
    }
}
