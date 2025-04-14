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
    public static int RESOLUTION = 0;
    public static int LANGUAGE = 0;

    private List<Resolution> presetResolutions = new List<Resolution>();
    private Resolution[] resolutions;

    private Slider sliderMusic;
    private Slider sliderSfx;
    private TMP_Dropdown dropdownResolution;
    private TMP_Dropdown dropdownLanguage;
    private Toggle toggleMuted;
    private Toggle toggleFullscreen;
    private GameObject _settings;

    private LanguageManager _languageManager;
    private bool isSettingLanguage = false;

    private void Awake()
    {
        _languageManager = new LanguageManager();
        CacheUIReferences();
        ApplySettingsToUI();
        SetTranslatedTexts();

        Screen.fullScreen = FULLSCREEN;
        SetResolution();
    }

    void Start()
    {
        if (_settings != null) _settings.SetActive(false);
    }

    private void CacheUIReferences()
    {
        toggleMuted = FindUI<Toggle>("_muted");
        toggleFullscreen = FindUI<Toggle>("_fullscreen");
        sliderMusic = FindUI<Slider>("_music");
        sliderSfx = FindUI<Slider>("_sfx");
        dropdownResolution = FindUI<TMP_Dropdown>("_resolution");
        dropdownLanguage = FindUI<TMP_Dropdown>("_language");
        _settings = GameObject.FindWithTag("_settings");

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
        if (sliderMusic != null) sliderMusic.value = MUSICVOLUME * 2f;
        if (sliderSfx != null) sliderSfx.value = SFXVOLUME;
        if (dropdownResolution != null) dropdownResolution.value = RESOLUTION;
        if (dropdownLanguage != null) dropdownLanguage.value = LANGUAGE;
    }

    private void SetTranslatedTexts()
    {
        string[] texts = GetTranslatedTexts();

        GameObject.FindWithTag("_settingsTag").GetComponent<TextMeshProUGUI>().text = texts[0];
        toggleFullscreen.GetComponentInChildren<Text>().text = texts[1];
        toggleMuted.GetComponentInChildren<Text>().text = texts[2];
        sliderMusic.GetComponentInChildren<TextMeshProUGUI>().text = texts[3];
        sliderSfx.GetComponentInChildren<TextMeshProUGUI>().text = texts[4];
        GameObject.FindWithTag("_backButton").GetComponent<TextMeshProUGUI>().text = texts[11];

        SetResolutionTexts(texts);
        SetLanguageTexts(texts);
    }

    private string[] GetTranslatedTexts()
    {
        string[] texts = _languageManager.GetSettingsTexts();
        if (texts == null || texts.Length == 0)
        {
            Debug.LogWarning("No language texts available.");
        }
        return texts;
    }

    private void SetResolutionTexts(string[] texts)
    {
        List<string> resolutionsText = new List<string>();
        for (int i = 5; i < 9; i++) resolutionsText.Add(texts[i]);

        if (dropdownResolution != null)
        {
            dropdownResolution.ClearOptions();
            dropdownResolution.AddOptions(resolutionsText);
            dropdownResolution.value = Mathf.Clamp(RESOLUTION, 0, resolutionsText.Count - 1);
            dropdownResolution.RefreshShownValue();
        }
    }

    private void SetLanguageTexts(string[] texts)
    {
        List<string> languageText = new List<string>();
        for (int i = 9; i < 11; i++) languageText.Add(texts[i]);

        if (dropdownLanguage != null)
        {
            dropdownLanguage.ClearOptions();
            dropdownLanguage.AddOptions(languageText);
            dropdownLanguage.value = Mathf.Clamp(LANGUAGE, 0, languageText.Count - 1);
            dropdownLanguage.RefreshShownValue();
        }
    }

    private void InitializeResolutions()
    {
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Muted: {MUTED}, Music: {MUSICVOLUME}, SFX: {SFXVOLUME}, Fullscreen: {FULLSCREEN}, Lang: {LANGUAGE}");
        }
        if(_settings != null && Cursor.visible == false)
            Cursor.visible = true;
    }

    public void Mute()
    {
        if (toggleMuted != null)
            MUTED = toggleMuted.isOn;
    }

    public void MusicVolume()
    {
        if (sliderMusic != null)
            MUSICVOLUME = sliderMusic.value / 2f;
    }

    public void SfxVolume()
    {
        if (sliderSfx != null)
            SFXVOLUME = sliderSfx.value;
    }

    public void Fullscreen()
    {
        if (toggleFullscreen != null)
        {
            FULLSCREEN = toggleFullscreen.isOn;
            Screen.fullScreen = FULLSCREEN;
        }
    }

    public void SetResolution()
    {
        if (dropdownResolution != null && presetResolutions.Count >= 4)
        {
            RESOLUTION = dropdownResolution.value;
            Resolution res = presetResolutions[RESOLUTION];
            Screen.SetResolution(res.width, res.height, FULLSCREEN);
        }
    }

    public void SetLanguage()
    {
        if(_settings != null)
        {
            if (isSettingLanguage) return;

            isSettingLanguage = true;

            LANGUAGE = dropdownLanguage.value;
            SetTranslatedTexts();

            isSettingLanguage = false;
        }
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

    public void ToggleSettings()
    {
        if (_settings != null)
            _settings.SetActive(false);
        Cursor.visible = false;
    }
}
