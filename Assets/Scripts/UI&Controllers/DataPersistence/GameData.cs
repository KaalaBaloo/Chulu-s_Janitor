using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Save Points
    public int levelsUnlocked;

    //General Settings
    public bool _muted;
    public bool _fullscreen;
    public float _musicVolume;
    public float _sfxVolume;

    public GameData() 
    { 
        this.levelsUnlocked = 0;

        _muted = false;
        _fullscreen = true;
        _musicVolume = 50;
        _sfxVolume = 50;
    }

}
