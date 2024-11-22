using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //bruh this code is a mess
    [SerializeField] private GameObject CamHolder;
    [SerializeField] private float sprintSpeed, walkSpeed, jumpForce, realJumpForce, smoothTime;
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private int mouseSensitivity;
    public bool hasHammer;
    public bool hasGlasses;
    private float verticalLookRotation;
    private bool Grounded;
    private Vector3 smoothMoveVelocity;
    private Vector3 moveAmount;

    private Rigidbody rb;

    [SerializeField] private GameObject hammer;

    public GameObject realThings;
    private GameObject unrealThings;
    
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask attackable;

    public GameObject glassEffect;

    //i dont even need this
    public Animator playerAnim;
    public bool hasKey;
    public bool looksKey;
    public bool looksButton;
    public bool looksHammer;
    public bool looksGlasses;


    private GameManager gm;
    public bool glassesOn;
    public bool areGlassesOn;
    
    public float maxHealth = 100f;
    public float currentHealth;
    private void Awake()
    {
        currentHealth = maxHealth;
        Time.timeScale = 1;
        gm = GameManager.instance;

        hasKey = false;
        rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        mouseSensitivity = PlayerPrefs.GetInt("sensitivity");

        
    }
    
    private float glassTime;

    public GameObject equipSound;
    public GameObject buttonSound;
    

    private void Start()
    {
        areGlassesOn = false;
        realThings = GameObject.FindGameObjectWithTag("real");
        unrealThings = GameObject.FindGameObjectWithTag("unreal");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gm.difficulty);
            
        if (gm.hasGlasses)
        {
            glassesOn = Input.GetMouseButton(1);
        }

        if (currentHealth <= 0)
        {
            Die();
        }

        glassEffect.SetActive(areGlassesOn);
        realThings.SetActive(areGlassesOn);
        if (unrealThings != null)
        {
            unrealThings.SetActive(!areGlassesOn);
        }
        
        var ray = new Ray(CamHolder.transform.position, CamHolder.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, objectLayer))
        {
            if (hit.transform.gameObject.CompareTag("Key"))
            {
                looksKey = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(equipSound, gameObject.transform.position, Quaternion.identity);
                    hasKey = true;
                }
            }

            if (hit.transform.gameObject.CompareTag("!Key"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Key key = hit.transform.GetComponent<Key>();
                    if (key != null)
                    {
                        key.hasFakeKey = true;
                    }
                }
            }

            if (hit.transform.gameObject.CompareTag("hammer"))
            {
                looksHammer = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(equipSound, gameObject.transform.position, Quaternion.identity);
                    gm.hasHammer = true;
                }
            }

            if (hit.transform.gameObject.name == "Glassesled")
            {
                looksGlasses = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(equipSound, gameObject.transform.position, Quaternion.identity);
                    if (!gm.hasGlasses)
                    {
                        gm.hasGlasses = true;
                    }
                }
            }

            if (hit.transform.gameObject.CompareTag("jump"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(equipSound, gameObject.transform.position, Quaternion.identity);
                    gm.hasJump = true;
                }
            }

            if (hit.transform.gameObject.GetComponent<ButtonGameObject>() == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(buttonSound, gameObject.transform.position, Quaternion.identity);
                    hit.transform.gameObject.GetComponent<ButtonGameObject>().doorIsOpening = true;
                }
            }
        }

        Look();
        Move();
        Jump();

        if (gm.hasHammer)
        {
            hammer.SetActive(true);
        }
        else
        {
            hammer.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= NextTimeToFire)
        {
            
            if (gm.hasHammer)
            {
                NextTimeToFire = Time.time + 1f / fireRate;
                Damage();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (gm.hasHammer)
            {
                playerAnim.SetTrigger("StopHit");
            }
        }
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        CamHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount,
            moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            if (glassesOn)
            {
                if (gm.hasJump)
                {
                    rb.AddForce(transform.up * realJumpForce);
                }
                else
                {
                    rb.AddForce(transform.up * jumpForce);
                }
            }
            else
            {
                rb.AddForce(transform.up * jumpForce);
            }


        }
    }

    public void SetGroundedState(bool _grounded)
    {
        Grounded = _grounded;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public float fireRate;
    private float NextTimeToFire;
    void Damage()
    {
        playerAnim.SetTrigger("Hit");
        
        Collider[] hitObjects = Physics.OverlapSphere(attackPoint.position, attackRange, attackable);

        foreach (var objects in hitObjects)
        {
            if (objects.GetComponent<ObjectHealth>() != null)
            {
                if (!Grounded)
                {
                    if (gm.difficulty == 1)
                    {
                        objects.GetComponent<ObjectHealth>().objectHealth -= 50;
                    }else if (gm.difficulty == 2)
                    {
                        objects.GetComponent<ObjectHealth>().objectHealth -= 30;
                    }else if (gm.difficulty == 3)
                    {
                        objects.GetComponent<ObjectHealth>().objectHealth -= 10;
                    }
                }
                else
                {
                    if (gm.difficulty == 1)
                    {
                        objects.GetComponent<ObjectHealth>().objectHealth -= 30;
                    }else if (gm.difficulty == 2)
                    {
                        objects.GetComponent<ObjectHealth>().objectHealth -= 10;
                    }else if (gm.difficulty == 3)
                    {
                        objects.GetComponent<ObjectHealth>().objectHealth -= 4;
                    }
                }
            }
        }
    }

    public void Die()
    {
        CanvasManager cm = FindObjectOfType<CanvasManager>();
        
        cm.Die();
    }



}