using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSettings : MonoBehaviour
{
    static public bool MUTED = false;
    static public float MUSICVOLUME = 100;
    static public float SFXVOLUME = 100;
    Slider _sliderMusic;
    Slider _sliderSfx;

    void Start()
    {
        _sliderMusic = GameObject.FindWithTag("_music").GetComponent<Slider>();
        _sliderMusic.value = MUSICVOLUME;
        _sliderSfx = GameObject.FindWithTag("_sfx").GetComponent<Slider>();
        _sliderSfx.value = SFXVOLUME;
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

}



