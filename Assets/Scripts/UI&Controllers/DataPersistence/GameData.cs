using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Save Points
    public int _levelsUnlocked;

    //General Settings
    public bool _muted;
    public bool _fullscreen;
    public float _musicVolume;
    public float _sfxVolume;
    public int _resolution;

    public GameData() 
    { 
        _levelsUnlocked = 0;
        _muted = false;
        _fullscreen = true;
        _musicVolume = 30;
        _sfxVolume = 50;
        _resolution = 5;
    }

}
