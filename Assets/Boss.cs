using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Boss : MonoBehaviour
{
    private GameManager gm;
    
    public GameObject[] projectile;
    public ParticleSystem particles;
    public Transform tip;
    public Transform playerPos;
    public GameObject turretHead;
    public float range;
    public float fireRate;
    public float firerateNazi;
    private float NextTimeToFire;
    public GameObject parent;

    [SerializeField] private ObjectHealth oh;
    
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        phase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (oh.objectHealth <= 27500)
        {
            phase = 2;
        }
        if(oh.objectHealth <= 13000)
        {
            phase = 3;
        }

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

        if (phase == 1)
        {
            u = 1;
            fireRate = 1;
            firerateNazi = 2;
        }else if (phase == 2)
        {
            firerateNazi = 3;
            u = 0;
            fireRate = 2;
        }else if (phase == 3)
        {
            firerateNazi = 4;
            fireRate = 3;
            u = rdm.Next(projectile.Length);
        }

    }

    private Random rdm = new Random();
    private int phase;
    private int u;

    void Shoot()
    {
        particles.Play();
        var position = tip.position;
        (Instantiate(projectile[u], position, Quaternion.identity) as GameObject).transform.parent = parent.transform;
    }
}
