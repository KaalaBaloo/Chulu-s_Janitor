using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    private GameObject _fadeBlack;
    private GameObject _settings;
    private SpriteRenderer _fadeBlackRenderer;
    private LanguageManager _languageManager;
    private int _language;

    private void Awake()
    {
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        _settings = GameObject.FindWithTag("_settings");
        _fadeBlackRenderer = _fadeBlack.GetComponent<SpriteRenderer>();
        _languageManager = new LanguageManager();
        _language = GeneralSettings.LANGUAGE;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main") StartCoroutine(FadeFromBlack());
    }

    private void Update()
    {
        if(_language != GeneralSettings.LANGUAGE)
        {
            _language = GeneralSettings.LANGUAGE;
            if (SceneManager.GetActiveScene().name == "Main") SetMainLanguage();
        }
    }

    private void SetMainLanguage()
    {
        string[] texts = GetTranslatedTexts();
        Button[] mainButtons = GameObject.FindWithTag("_mainButtons").GetComponentsInChildren<Button>();

        for (int i = 0; i < mainButtons.Length && i < texts.Length; i++)
        {
            TextMeshProUGUI textComponent = mainButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = texts[i];
            }
        }
    }


    private string[] GetTranslatedTexts()
    {
        string[] texts = _languageManager.GetMainMenuTexts();
        if (texts == null || texts.Length == 0)
        {
            Debug.LogWarning("No language texts available.");
        }
        return texts;
    }

    public void Restart()
    {
        StartCoroutine(FadeToBlack(SceneManager.GetActiveScene().name));
    }

    public void Menu()
    {
        StartCoroutine(FadeToBlack("LevelSelector"));
    }

    public void Comic()
    {
        StartCoroutine(FadeToBlack("Comic_1"));
    }

    public void LevelSelector()
    {
        string sceneToLoad = GridController.LEVELS_UNLOCKED == 0 ? "Comic_1" : "LevelSelector";
        StartCoroutine(FadeToBlack(sceneToLoad));
    }

    public void ToggleSettings()
    {
        _settings.SetActive(!_settings.activeSelf);
    }

    public void Credits()
    {
        StartCoroutine(FadeToBlack("End"));
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator FadeToBlack(string scene, int fadeSpeed = 5)
    {
        yield return Fade(fadeSpeed, fadeToBlack: true);
        SaveGame();
        SceneManager.LoadScene(scene);
    }

    private IEnumerator FadeFromBlack(int fadeSpeed = 8)
    {
        yield return Fade(fadeSpeed, fadeToBlack: false);
    }

    private IEnumerator Fade(int fadeSpeed, bool fadeToBlack)
    {
        Color color = _fadeBlackRenderer.color;
        float fadeAmount;

        while ((fadeToBlack && color.a < 1) || (!fadeToBlack && color.a > 0))
        {
            fadeAmount = fadeToBlack ? color.a + (fadeSpeed * Time.deltaTime) : color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlackRenderer.color = color;
            yield return null;
        }
    }

    private void SaveGame()
    {
        GameObject.FindGameObjectWithTag("_save").GetComponent<DataPersistenceManager>().SaveGame();
    }
}
