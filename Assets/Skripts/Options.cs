using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class Options : MonoBehaviour
{
    private int boolToInt(bool val)
    {
        if (val)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private bool intToBool(int var)
    {
        if (var != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isPPOn;
    public bool isAudioOn;
    public PostProcessVolume ppvolume;
    public AudioSource audio;
    public Slider sensitivitySlider;
    public Toggle postToggle;
    public Toggle audioToggle;
    public int sensitivity;
    
    // Start is called before the first frame update
    void Awake()
    {
        ppvolume = FindObjectOfType<PostProcessVolume>();
        
        isPPOn = intToBool(PlayerPrefs.GetInt("ppOn", 1));
        sensitivitySlider.value = PlayerPrefs.GetInt("sensitivity", 5);
    }

    // Update is called once per frame
    void Update()
    {
        isAudioOn = audioToggle.isOn;
        isPPOn = postToggle.isOn;

        if (audio != null)
        {
            audio.enabled = isAudioOn;
        }
        else
        {
            audio = FindObjectOfType<AudioSource>();
        }



        ppvolume.enabled = isPPOn;
        sensitivity = (int) sensitivitySlider.value;
        PlayerPrefs.SetInt("sensitivity", sensitivity);
        PlayerPrefs.SetInt("ppOn", boolToInt(isPPOn));
    }

    public void PostProcess()
    {
        isPPOn = !isPPOn;
    }
}
