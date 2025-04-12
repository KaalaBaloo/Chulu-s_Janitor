using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour, IDataPersistence
{
    public List<string> sceneNames;
    public string instanceName;
    AudioSource _music;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        _music = GetComponent<AudioSource>();    
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckForDuplicateInstances();

        CheckIfSceneInList();
    }

    void CheckForDuplicateInstances()
    {
        Music[] collection = FindObjectsOfType<Music>();

        foreach (Music obj in collection)
        {
            if (obj != this)
            {
                if (obj.instanceName == instanceName)
                {
                    Debug.Log("Duplicate object in loaded scene, deleting now...");
                    DestroyImmediate(obj.gameObject);
                }
            }
        }
    }
    void CheckIfSceneInList()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (!sceneNames.Contains(currentScene))
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            DestroyImmediate(this.gameObject);
        } 
    }

    private void Update()
    {
        if (GeneralSettings.MUTED)
        {
            _music.mute = true;
        }
        else
        {
            _music.mute = false;
            _music.volume = GeneralSettings.MUSICVOLUME / 100;
        }
    }

    public void LoadData(GameData data)
    {
        GridController.LEVELS_UNLOCKED = data.LevelsUnlocked;
        GeneralSettings.MUSICVOLUME = data.MusicVolume;
        GeneralSettings.SFXVOLUME = data.SfxVolume;
        GeneralSettings.FULLSCREEN = data.Fullscreen;
        GeneralSettings.MUTED = data.Muted;
    }

    public void SaveData(ref GameData data)
    {
        data.LevelsUnlocked = GridController.LEVELS_UNLOCKED;
        data.MusicVolume = GeneralSettings.MUSICVOLUME;
        data.SfxVolume = GeneralSettings.SFXVOLUME;
        data.Fullscreen = GeneralSettings.FULLSCREEN;
        data.Muted = GeneralSettings.MUTED;
    }


}
