using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private PlayerController pc;

    private GameManager gm;


    public GameObject keyIcon;
    public GameObject hammerIcon;

    public GameObject pauseMenu;
    public GameObject dieMenu;

    // Start is called before the first frame update
    void Awake()
    {
        
        gm = FindObjectOfType<GameManager>();
        pc = FindObjectOfType<PlayerController>();
        pc.enabled = true;
        Resume();
        keyIcon.SetActive(false);
        hammerIcon.SetActive(false);

        pauseMenu.SetActive(false);
        dieMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (pc.hasKey)
        {
            keyIcon.SetActive(true);
        }

        if (pc.hasHammer)
        {
            hammerIcon.SetActive(true);
        }
    }

    private bool paused;

    public void Pause()
    {
        pc.enabled = false;
        paused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pc.enabled = true;
        paused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void Die()
    {
        pc.enabled = false;
        dieMenu.SetActive(true);
        Time.timeScale = 0.4f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        if (gm.difficulty == 1) // easy
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }else if (gm.difficulty == 2) //difficult
        {
            SceneManager.LoadScene("dead");
        }else if (gm.difficulty == 3) // asian
        {
            SceneManager.LoadScene("dead");
        }

        
    }
}


