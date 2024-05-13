using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    GameObject _fadeBlack;
    GameObject _pause;
    GameObject _settings;

    private void Start()
    {
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        _pause = GameObject.FindWithTag("_pause");
        _settings = GameObject.FindWithTag("_settings");

        if (SceneManager.GetActiveScene().name == "Main")
        {
            StartCoroutine(FadefromBlack());
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main" && Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void Restart()
    {
        StartCoroutine(FadetoBlack(SceneManager.GetActiveScene().name));      
    }

    public void Menu()
    {
        StartCoroutine(FadetoBlack("LevelSelector"));
    }

    public void Comic()
    {
        StartCoroutine(FadetoBlack("Comic_1"));
    }

    public void LevelSelector()
    {
        if(GridController.LEVELS_UNLOCKED == 0)
        {
            StartCoroutine(FadetoBlack("Comic_1"));
        }
        else
        {
            StartCoroutine(FadetoBlack("LevelSelector"));
        }
    }

    public void Settings()
    {
        if(!_settings.activeSelf)
            _settings.SetActive(true);
        else
            _settings.SetActive(false);
    }

    public void Credits()
    {
        StartCoroutine(FadetoBlack("End"));
    }

    protected IEnumerator FadetoBlack(string scene, int fadeSpeed = 5)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        GameObject.FindGameObjectWithTag("_save").GetComponent<DataPersistenceManager>().SaveGame();
        SceneManager.LoadScene(scene);
        yield return null;
    }

    protected IEnumerator FadefromBlack(int fadeSpeed = 8)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }

}
