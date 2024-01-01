using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject _fadeBlack;
    AudioSource _audio;

    private void Start()
    {
        StartCoroutine(FadefromBlack());
        _audio = GetComponent<AudioSource>();
    }

    public void Levels()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("SelectorNiveles"));
    }

    public void Level1()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("1"));
    }
    public void Level2()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("2"));
    }
    public void Level3()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("3"));
    }
    public void Level4()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("4"));
    }
    public void Level5()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("5"));
    }
    public void Level6()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("6"));
    }
    public void Level7()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("7")); 
    }
    public void Level8()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("8"));
    }
    public void Level9()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("9"));
    }
    public void Level10()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("10"));
    }
    public void Level11()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("11"));
    }
    public void Level12()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("12"));
    }
    public void Level13()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("13"));
    }
    public void Level14()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("14"));
    }
    public void Level15()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("15"));
    }
    public void Level16()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("16"));
    }
    public void Level17()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("17"));
    }
    public void Level18()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("18"));
    }
    public void Level19()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("19"));
    }
    public void Level20()
    {
        _audio.Play();
        StartCoroutine(FadetoBlack("20"));
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
