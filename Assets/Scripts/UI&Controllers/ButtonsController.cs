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
    }

    public void Restart()
    {
        StartCoroutine(FadetoBlack(SceneManager.GetActiveScene().name));      
    }

    public void Menu()
    {
        StartCoroutine(FadetoBlack("LevelSelector"));
    }

    public void LevelSelector()
    {
        if(GridController.LEVELS_UNLOCKED == 1)
        {
            StartCoroutine(FadetoBlack("Comic_1"));
        }
        else
        {
            StartCoroutine(FadetoBlack("LevelSelector"));
        }
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

        SceneManager.LoadScene(scene);
        yield return null;
    }
}
