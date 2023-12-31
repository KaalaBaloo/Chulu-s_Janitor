using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    float t = 0;
    [SerializeField] GameObject _fadeBlack;
    [SerializeField] GameObject _credits;
    [SerializeField] GameObject _thanks;
    [SerializeField] float _creditsVel = 0.05f;

    bool _thanksActive = false;

    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(FadefromBlack());
    }

    virtual protected IEnumerator EndingCoroutine()
    {
        while (t < 25)
        {
            _credits.transform.position += new Vector3(0, _creditsVel, 0);
            t += Time.deltaTime;
            yield return null;
            if(t > 20 && !_thanksActive)
            {
                StartCoroutine(Thanks());
            }
        }
        StartCoroutine(FadetoBlack("Main"));
        yield return 0;
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
        StartCoroutine(EndingCoroutine());
    }

    protected IEnumerator Thanks(int fadeSpeed = 1)
    {
        Color color = _thanks.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (_thanks.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _thanks.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
        yield return null;
    }

}
