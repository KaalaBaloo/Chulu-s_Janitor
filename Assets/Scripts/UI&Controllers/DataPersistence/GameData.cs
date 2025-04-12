using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Save Points
    public int LevelsUnlocked;

    //General Settings
    public bool Muted;
    public bool Fullscreen;
    public float MusicVolume;
    public float SfxVolume;
    public int Resolution;
    public int Language;

    public GameData() 
    { 
        LevelsUnlocked = 0;
        Muted = false;
        Fullscreen = true;
        MusicVolume = 30;
        SfxVolume = 50;
        Resolution = 5;
        Language = 0;
    }

}
