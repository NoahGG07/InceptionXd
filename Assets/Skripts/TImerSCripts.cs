using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImerSCripts : MonoBehaviour
{
    private GameManager gm;
    private PlayerController pc;
    private Image imageBar;
    public float maxTime = 3f;
    public float maxTime2 = 2f;
    public float maxTime3 = 1f;
    private float timeleft;

    // Start is called before the first frame update
    
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pc = FindObjectOfType<PlayerController>();
        imageBar = GetComponent<Image>();
        
        if (gm.difficulty == 1)
        {
            timeleft = maxTime;
        }
        else if (gm.difficulty == 2)
        {
            timeleft = maxTime2;
        }else if (gm.difficulty == 3)
        {
            timeleft = maxTime3;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            if (gm.difficulty == 1)
            {
                    if (timeleft <= maxTime && pc.areGlassesOn == false)
                    {
                        timeleft += Time.deltaTime;
                    }
            }
            else if (gm.difficulty == 2) 
            {
                    if (timeleft <= maxTime2 && pc.areGlassesOn == false)
                    {
                        timeleft += Time.deltaTime;
                    } 
            }
            else if (gm.difficulty == 3) 
            {
                    if (timeleft <= maxTime3 && pc.areGlassesOn == false)
                    {
                        timeleft += Time.deltaTime;
                    } 
            }
            
        }


        if (gm.difficulty == 1)
        {
            imageBar.fillAmount = timeleft / maxTime;
        }
        else if (gm.difficulty == 2)
        {
            imageBar.fillAmount = timeleft / maxTime2;
        }
        else if (gm.difficulty == 3)
        {
            imageBar.fillAmount = timeleft / maxTime3;
        }



        if (pc.glassesOn)
        {
            if (timeleft > 0)
            {
                pc.areGlassesOn = true;
                timeleft -= Time.deltaTime;
                
            }
            else
            {
                pc.areGlassesOn = false;
            }
        }
        else
        {
            pc.areGlassesOn = false;
        }


    }
    
    
}
