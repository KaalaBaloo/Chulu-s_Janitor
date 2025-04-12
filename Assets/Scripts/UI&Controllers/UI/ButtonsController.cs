using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    private GameObject _fadeBlack;
    private GameObject _pause;
    private GameObject _settings;
    private SpriteRenderer _fadeBlackRenderer;

    private void Start()
    {
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        _pause = GameObject.FindWithTag("_pause");
        _settings = GameObject.FindWithTag("_settings");

        _fadeBlackRenderer = _fadeBlack.GetComponent<SpriteRenderer>();

        if (SceneManager.GetActiveScene().name == "Main")
        {
            StartCoroutine(FadeFromBlack());
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main" && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
