using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public List<string> sceneNames;
    public string instanceName;
    AudioSource _music;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        _music = GetComponent<AudioSource>();    
        if (GeneralSettings.MUTED)
        {
            _music.volume = 0;
            _music.mute = true;
        }
        else
        {
            _music.mute = false;
            _music.volume = GeneralSettings.MUSICVOLUME / 100;
        }
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
            _music.volume = 0;
            _music.mute = true;
        }
        else
        {
            _music.mute = false;
            _music.volume = GeneralSettings.MUSICVOLUME / 100;
        }
    }

}
