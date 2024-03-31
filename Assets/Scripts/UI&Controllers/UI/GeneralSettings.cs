using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSettings : MonoBehaviour, IDataPersistence
{
    static public bool MUTED;
    static public bool FULLSCREEN;
    static public float MUSICVOLUME;
    static public float SFXVOLUME;
    Resolution[] resolutions;
    int _resolution;
    //0_Low - 1_Medium - 2_High - 3_VeryHigh - 4_Ultra 
    static public int LANGUAGE;
    int _language;
    //0_Spanish - 1_English

    Slider _sliderMusic;
    Slider _sliderSfx;
    TMP_Dropdown _dropdownResolution;
    TMP_Dropdown _dropdownLanguage;
    Toggle _toggleMuted;
    Toggle _toggleFullscreen;

    void Start()
    {
        _toggleMuted = GameObject.FindWithTag("_muted").GetComponent<Toggle>();
        _toggleMuted.isOn = MUTED;
        _toggleFullscreen = GameObject.FindWithTag("_fullscreen").GetComponent<Toggle>();
        _toggleFullscreen.isOn = FULLSCREEN;
        _sliderMusic = GameObject.FindWithTag("_music").GetComponent<Slider>();
        _sliderMusic.value = MUSICVOLUME;
        _sliderSfx = GameObject.FindWithTag("_sfx").GetComponent<Slider>();
        _sliderSfx.value = SFXVOLUME;
        _dropdownResolution = GameObject.FindWithTag("_resolution").GetComponent<TMP_Dropdown>();
        resolutions = Screen.resolutions;
        _dropdownResolution.value = _resolution;
        _dropdownLanguage = GameObject.FindWithTag("_language").GetComponent<TMP_Dropdown>();
        _dropdownLanguage.value = _language;
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

    public void LoadData(GameData data)
    {
        MUSICVOLUME = data._musicVolume;
        SFXVOLUME = data._sfxVolume;
        FULLSCREEN = data._fullscreen;
        MUTED = data._muted;
    }
    
    public void SaveData(ref GameData data) 
    {
       data._musicVolume = MUSICVOLUME;
       data._sfxVolume = SFXVOLUME;
       data._fullscreen = FULLSCREEN;
       data._muted = MUTED;
    }

}



