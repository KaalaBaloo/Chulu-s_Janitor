using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ComicDefault : MonoBehaviour
{
    [SerializeField] Image _image;
    GameObject _fadeBlack;
    [SerializeField] Sprite[] _sprites;
    int _spriteIndex = 0;
    [SerializeField] int[] _changeAuto;
    int _changeIndex = 0;
    bool _autoPlay = true;
    [SerializeField] string _nextScene;

    void Start()
    {
        _spriteIndex = 0;
        _image.sprite = _sprites[_spriteIndex];
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        StartCoroutine(FadefromBlack());
    }

    void Update()
    {
        if (!_autoPlay && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _spriteIndex++;
            StartCoroutine(FadetoBlack());
        }
    }

    protected IEnumerator ComicPlay()
    {
        float t = 0;
        while (t < 2)
        {
            t += Time.deltaTime;
            yield return null;
        }
        _spriteIndex++;
        if (_spriteIndex == _changeAuto[_changeIndex])
        {
            _changeIndex++;
            _autoPlay = false;
        }
        else
            _autoPlay = true;
        StartCoroutine(FadetoBlack());
        yield return null;
    }

    protected IEnumerator FadetoBlack(int fadeSpeed = 2)
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
        _image.sprite = _sprites[_spriteIndex];
        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        if (_spriteIndex - 1 < _sprites.Length && _autoPlay)
            StartCoroutine(ComicPlay());
        else
            StartCoroutine(FadetoBlackScene(_nextScene));

        yield return null;
    }

    protected IEnumerator FadetoBlackScene(string scene, int fadeSpeed = 5)
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

        StartCoroutine(ComicPlay());
    }
}
