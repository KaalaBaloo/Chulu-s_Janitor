using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
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

    public void Continue()
    {
        Cursor.visible = false;
        _pause.SetActive(false);
    }

    public void Restart()
    {
        StartCoroutine(FadetoBlack(SceneManager.GetActiveScene().name));      
    }

    public void Menu()
    {
        StartCoroutine(FadetoBlack("SelectorNiveles"));
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
