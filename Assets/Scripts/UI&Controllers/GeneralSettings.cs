using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSettings : MonoBehaviour
{
    static public bool MUTED = false;
    static public bool FULLSCREEN = true;
    static public float MUSICVOLUME = 50;
    static public float SFXVOLUME = 50;
    Resolution[] resolutions;
    //0_Low - 1_Medium - 2_High - 3_VeryHigh - 4_Ultra 
    static public int LANGUAGE = 0;
    //0_Spanish - 1_English
    Slider _sliderMusic;
    Slider _sliderSfx;
    TMP_Dropdown _dropdownResolution;
    TMP_Dropdown _dropdownLanguage;
    GameObject _pause;
    GameObject _settings;

    void Start()
    {
        _sliderMusic = GameObject.FindWithTag("_music").GetComponent<Slider>();
        _sliderMusic.value = MUSICVOLUME;
        _sliderSfx = GameObject.FindWithTag("_sfx").GetComponent<Slider>();
        _sliderSfx.value = SFXVOLUME;
        _dropdownResolution = GameObject.FindWithTag("_resolution").GetComponent<TMP_Dropdown>();
        _dropdownLanguage = GameObject.FindWithTag("_language").GetComponent<TMP_Dropdown>();
        _pause = GameObject.FindWithTag("_pause");
        _settings = GameObject.FindWithTag("_settings");
        resolutions = Screen.resolutions;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(MUTED);
            Debug.Log(MUSICVOLUME);
            Debug.Log(SFXVOLUME);
            Debug.Log(FULLSCREEN);
            Debug.Log(LANGUAGE);
        }
    }

    public void Mute()
    {
        if (MUTED) 
        {
            MUTED = false;
        }
        else
        {
            MUTED = true;
        }
    }

    public void MusicVolume()
    {
        MUSICVOLUME = _sliderMusic.value;
    }

    public void SfxVolume()
    {
        SFXVOLUME = _sliderSfx.value;
    }

    public void Fullscreen()
    {
        if (!FULLSCREEN)
        {
            Screen.fullScreen = true;
            FULLSCREEN = true;
        }
        else
        {
            Screen.fullScreen = false;
            FULLSCREEN= false;
        }
    }

    public void SetResolution()
    {
        Resolution resolution = resolutions[_dropdownResolution.value];
        Screen.SetResolution(resolution.width,
                  resolution.height, Screen.fullScreen);
    }

    public void SetLanguage()
    {
        LANGUAGE = _dropdownLanguage.value;
    }
}



