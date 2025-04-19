using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour, IDataPersistence
{
    public static MusicManager Instance;

    private AudioSource _music;
    private AudioClip _clip;

    private DialogueController _dialogue;

    private void Awake()
    {
        // Singleton logic
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _music = GetComponent<AudioSource>();

        if (SoundManager.Instance != null)
        {
            PlayLevelMusic();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayLevelMusic()
    {
        _clip = SoundManager.Instance.GetLevelMusic(SceneManager.GetActiveScene().name);
        _music.clip = _clip;
        _music.Play();
    }

    private void Update()
    {
        _music.mute = GeneralSettings.MUTED;
        if (!GeneralSettings.MUTED)
        {
            _music.volume = GeneralSettings.MUSICVOLUME / 100f;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentScene = scene.name;

        _clip = SoundManager.Instance?.GetLevelMusic(currentScene);
        if (_clip != null && _clip != _music.clip)
        {
            _music.clip = _clip;
            _music.Play();
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

    public bool isNyarPlaying()
    {
        if (_music.clip == SoundManager.Instance.GetDialogueMusic(0))
            return true;
        else
            return false;
    }

    public void PlayNyarTheme()
    {
        _music.Stop();
        _music.clip = SoundManager.Instance.GetDialogueMusic(0);
        _music.Play();
    }

    public void PlayDialogueMusic()
    {
        _music.Stop();
        _music.clip = SoundManager.Instance.GetDialogueMusic(GetDialogueMusic());
        _music.Play();
    }

    private int GetDialogueMusic()
    {
        if (SceneManager.GetActiveScene().name == "20_Battle")
            return 2;
        else
            return 1;
    }

}
