using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private PlayerController pc;
    private Image healthBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = pc.currentHealth / pc.maxHealth;
    }
}
