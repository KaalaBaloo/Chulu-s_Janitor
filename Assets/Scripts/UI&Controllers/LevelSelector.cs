using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject _fadeBlack;

    private void Start()
    {
        StartCoroutine(FadefromBlack());
    }

    public void Levels()
    {
        StartCoroutine(FadetoBlack("SelectorNiveles"));
    }

    public void Level1()
    {
        StartCoroutine(FadetoBlack("1"));
    }
    public void Level2()
    {
        StartCoroutine(FadetoBlack("2"));
    }
    public void Level3()
    {
        StartCoroutine(FadetoBlack("3"));
    }
    public void Level4()
    {
        StartCoroutine(FadetoBlack("4"));
    }
    public void Level5()
    {
        StartCoroutine(FadetoBlack("5"));
    }
    public void Level6()
    {
        StartCoroutine(FadetoBlack("6"));
    }
    public void Level7()
    {
        StartCoroutine(FadetoBlack("7")); 
    }
    public void Level8()
    {
        StartCoroutine(FadetoBlack("8"));
    }
    public void Level9()
    {
        StartCoroutine(FadetoBlack("9"));
    }
    public void Level10()
    {
        StartCoroutine(FadetoBlack("10"));
    }
    public void Level11()
    {
        StartCoroutine(FadetoBlack("11"));
    }
    public void Level12()
    {
        StartCoroutine(FadetoBlack("12"));
    }
    public void Level13()
    {
        StartCoroutine(FadetoBlack("13"));
    }
    public void Level14()
    {
        StartCoroutine(FadetoBlack("14"));
    }
    public void Level15()
    {
        StartCoroutine(FadetoBlack("15"));
    }
    public void Level16()
    {
        StartCoroutine(FadetoBlack("16"));
    }
    public void Level17()
    {
        StartCoroutine(FadetoBlack("17"));
    }
    public void Level18()
    {
        StartCoroutine(FadetoBlack("18"));
    }
    public void Level19()
    {
        StartCoroutine(FadetoBlack("19"));
    }
    public void Level20()
    {
        StartCoroutine(FadetoBlack("20"));
    }

    protected IEnumerator FadetoBlack(string scene, int fadeSpeed = 8)
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
