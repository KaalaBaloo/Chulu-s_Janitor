using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralSettings : MonoBehaviour, IDataPersistence
{
    static public bool MUTED = false;
    static public bool FULLSCREEN = true;
    static public float MUSICVOLUME = 0.3f;
    static public float SFXVOLUME = 0.5f;
    static public int RESOLUTION = 5;
    //0_VeryLow - 1_Low - 2_Medium - 3_High - 4_VeryHigh - 5_Ultra
    static public int LANGUAGE = 0;
    //0_English - 1_Spanish

    Resolution[] resolutions;
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
        _dropdownResolution.value = RESOLUTION;
        //_dropdownLanguage = GameObject.FindWithTag("LANGUAGE").GetComponent<TMP_Dropdown>();
        //_dropdownLanguage.value = LANGUAGE;

        if (FULLSCREEN)
            Screen.fullScreen = true;
        else
            Screen.fullScreen = false;
        SetResolution();

        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameObject.FindWithTag("_settings").SetActive(false);
        }
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
        MUSICVOLUME = _sliderMusic.value/2;
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
        int finalResolution = Mathf.RoundToInt(resolutions.Length/6 * _dropdownResolution.value);

        Screen.SetResolution(resolutions[finalResolution].width, resolutions[finalResolution].height, FULLSCREEN);
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
        RESOLUTION = data._resolution;
    }
    
    public void SaveData(ref GameData data) 
    {
       data._musicVolume = MUSICVOLUME;
       data._sfxVolume = SFXVOLUME;
       data._fullscreen = FULLSCREEN;
       data._muted = MUTED;
       data._resolution = RESOLUTION;
    }

}



