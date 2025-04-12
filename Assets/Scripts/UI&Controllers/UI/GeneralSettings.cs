using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GeneralSettings : MonoBehaviour, IDataPersistence
{
    public static bool MUTED = false;
    public static bool FULLSCREEN = true;
    public static float MUSICVOLUME = 0.3f;
    public static float SFXVOLUME = 0.5f;
    public static int RESOLUTION = 5;
    public static int LANGUAGE = 0;

    private List<Resolution> presetResolutions = new List<Resolution>();
    private Resolution[] resolutions;
    private Slider sliderMusic;
    private Slider sliderSfx;
    private TMP_Dropdown dropdownResolution;
    private TMP_Dropdown dropdownLanguage;
    private Toggle toggleMuted;
    private Toggle toggleFullscreen;

    void Start()
    {
        CacheUIReferences();
        ApplySettingsToUI();
        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameObject settingsPanel = GameObject.FindWithTag("_settings");
            if (settingsPanel != null) settingsPanel.SetActive(false);
        }

        Screen.fullScreen = FULLSCREEN;
        SetResolution();
    }

    private void CacheUIReferences()
    {
        toggleMuted = FindUI<Toggle>("_muted");
        toggleFullscreen = FindUI<Toggle>("_fullscreen");
        sliderMusic = FindUI<Slider>("_music");
        sliderSfx = FindUI<Slider>("_sfx");
        dropdownResolution = FindUI<TMP_Dropdown>("_resolution");
        dropdownLanguage = FindUI<TMP_Dropdown>("_language");

        resolutions = Screen.resolutions;
        InitializeResolutions();
    }

    private T FindUI<T>(string tag) where T : Component
    {
        GameObject obj = GameObject.FindWithTag(tag);
        if (obj == null)
        {
            Debug.LogWarning($"UI element with tag '{tag}' not found.");
            return null;
        }
        return obj.GetComponent<T>();
    }

    private void ApplySettingsToUI()
    {
        if (toggleMuted != null) toggleMuted.isOn = MUTED;
        if (toggleFullscreen != null) toggleFullscreen.isOn = FULLSCREEN;
        if (sliderMusic != null) sliderMusic.value = MUSICVOLUME * 2;
        if (sliderSfx != null) sliderSfx.value = SFXVOLUME;
        if (dropdownResolution != null) dropdownResolution.value = RESOLUTION;
        if (dropdownLanguage != null) dropdownLanguage.value = LANGUAGE;
    }

    private void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        presetResolutions.Clear();

        if (resolutions == null || resolutions.Length == 0)
        {
            Debug.LogWarning("No resolutions available.");
            return;
        }

        List<Resolution> distinct = new List<Resolution>();
        HashSet<string> seen = new HashSet<string>();
        foreach (var res in resolutions)
        {
            string key = $"{res.width}x{res.height}";
            if (!seen.Contains(key))
            {
                seen.Add(key);
                distinct.Add(res);
            }
        }

        distinct.Sort((a, b) => (a.width * a.height).CompareTo(b.width * b.height));

        int count = distinct.Count;

        if (count >= 4)
        {
            presetResolutions.Add(distinct[0]);
            presetResolutions.Add(distinct[count / 3]);
            presetResolutions.Add(distinct[(count * 2) / 3]);
            presetResolutions.Add(distinct[count - 1]);
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                int index = Mathf.Clamp(i, 0, count - 1);
                presetResolutions.Add(distinct[index]);
            }

            Resolution highest = distinct[count - 1];
            for (int i = count; i < 4; i++)
            {
                presetResolutions[i] = highest;
            }
        }

        if (dropdownResolution != null)
        {
            dropdownResolution.ClearOptions();
            List<string> options = new List<string> { "Low Resolution", "Medium Resolution", "High Resolution", "Ultra Resolution" };
            dropdownResolution.AddOptions(options);
            dropdownResolution.value = Mathf.Clamp(RESOLUTION, 0, 3);
            dropdownResolution.RefreshShownValue();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Muted: {MUTED}, Music: {MUSICVOLUME}, SFX: {SFXVOLUME}, Fullscreen: {FULLSCREEN}, Lang: {LANGUAGE}");
        }
    }

    public void Mute() => MUTED = !MUTED;

    public void MusicVolume()
    {
        if (sliderMusic != null)
            MUSICVOLUME = sliderMusic.value / 2;
    }

    public void SfxVolume()
    {
        if (sliderSfx != null)
            SFXVOLUME = sliderSfx.value;
    }

    public void Fullscreen()
    {
        FULLSCREEN = !FULLSCREEN;
        Screen.fullScreen = FULLSCREEN;
    }

    public void SetResolution()
    {
        if (dropdownResolution == null || presetResolutions.Count < 4)
            return;

        int index = Mathf.Clamp(dropdownResolution.value, 0, presetResolutions.Count - 1);
        Resolution res = presetResolutions[index];

        Screen.SetResolution(res.width, res.height, FULLSCREEN);
    }

    public void SetLanguage()
    {
        if (dropdownLanguage != null)
            LANGUAGE = dropdownLanguage.value;
    }

    public void LoadData(GameData data)
    {
        MUSICVOLUME = data.MusicVolume;
        SFXVOLUME = data.SfxVolume;
        FULLSCREEN = data.Fullscreen;
        MUTED = data.Muted;
        RESOLUTION = data.Resolution;
        LANGUAGE = data.Language;

        if (sliderMusic == null || dropdownResolution == null)
            CacheUIReferences();

        ApplySettingsToUI();
        Screen.fullScreen = FULLSCREEN;
        SetResolution();
    }

    public void SaveData(ref GameData data)
    {
        data.MusicVolume = MUSICVOLUME;
        data.SfxVolume = SFXVOLUME;
        data.Fullscreen = FULLSCREEN;
        data.Muted = MUTED;
        data.Resolution = RESOLUTION;
        data.Language = LANGUAGE;
    }
}
