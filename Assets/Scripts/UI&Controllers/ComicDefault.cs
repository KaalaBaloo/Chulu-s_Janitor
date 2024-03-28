using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ComicDefault : MonoBehaviour
{
    [SerializeField] Image _image;
    GameObject _fadeBlack;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] int _spriteIndex = 0;
    [SerializeField] int[] _changeAuto;
    [SerializeField] int _changeIndex = 0;
    [SerializeField] bool _autoPlay = true;
    [SerializeField] string _nextScene;


    [SerializeField] GameObject _dialogueSquare;
    [SerializeField] GameObject _dialogueText;
    [SerializeField] GameObject _dialogueSprite;
    [SerializeField] TMP_Text _text;
    [SerializeField] string[] _dialoguesTexts;
    [SerializeField] Image _dialogueImage;
    [SerializeField] Sprite[] _dialogueImages;
    int _dialogueIndex = 0;

    [SerializeField] int[] _fadesBlack;
    [SerializeField] int _fadesIndex = 0;

    void Start()
    {
        _spriteIndex = 0;
        _image.sprite = _sprites[_spriteIndex];
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        StartCoroutine(FadefromBlack());
        _text.text = _dialoguesTexts[_dialogueIndex];
        _dialogueImage.sprite = _dialogueImages[_dialogueIndex];
        _dialogueSquare.SetActive(false);
        _dialogueText.SetActive(false);
        _dialogueSprite.SetActive(false);
    }

    void Update()
    {
        if (!_autoPlay && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _text.text = _dialoguesTexts[_dialogueIndex];
            _dialogueImage.sprite = _dialogueImages[_dialogueIndex];
            _dialogueIndex++;
            StartCoroutine(ComicPlay());
        }
    }

    protected IEnumerator ComicPlay()
    {
        float t = 0;
        int v = 2;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            yield return null;
        }
        _spriteIndex++;
        if(_changeAuto.Length >= _changeIndex + 1)
        {
            if (_spriteIndex == _changeAuto[_changeIndex])
            {
                _changeIndex++;
                _autoPlay = false;
                v = 5;
            }
            
        }
        else if (_changeAuto.Length < _changeIndex)
        {
            _autoPlay = false;
            v = 5;
        }
        else
        {
            _autoPlay = true;  
        }

        if(_autoPlay)
        {
            while (t < 1f)
            {
                t += Time.deltaTime;
                yield return null;
            }
        }

        if (_fadesBlack.Length >= _fadesIndex + 1)
        {
            if (_fadesBlack[_fadesIndex] == _spriteIndex)
            {
                _fadesIndex++;
                StartCoroutine(FadetoBlack(v));
            }
            else
                StartCoroutine(ChangeImage());
        }
        else
            StartCoroutine(ChangeImage());

        yield return null;
    }

    protected IEnumerator ChangeImage()
    {
        float t = 0;

        if (_spriteIndex < _sprites.Length)
            _image.sprite = _sprites[_spriteIndex];
        if (_dialogueIndex < _dialogueImages.Length && !_autoPlay)
        {
            _text.text = _dialoguesTexts[_dialogueIndex];
            _dialogueImage.sprite = _dialogueImages[_dialogueIndex];
            DialogueEnabled(true);
        }
        else
            DialogueEnabled(false);

        if (_autoPlay)
        {
            while (t < 1f)
            {
                t += Time.deltaTime;
                yield return null;
            }
        }

        if (_spriteIndex < _sprites.Length && _autoPlay)
            StartCoroutine(ComicPlay());
        else if (_spriteIndex >= _sprites.Length && _autoPlay)
            StartCoroutine(FadetoBlackScene(_nextScene));

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
        if (_spriteIndex < _sprites.Length)
            _image.sprite = _sprites[_spriteIndex];
        if (_dialogueIndex < _dialogueImages.Length && !_autoPlay)
        {
            _text.text = _dialoguesTexts[_dialogueIndex];
            _dialogueImage.sprite = _dialogueImages[_dialogueIndex];
            DialogueEnabled(true);
        }
        else
            DialogueEnabled(false);
        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        if (_spriteIndex < _sprites.Length && _autoPlay)
            StartCoroutine(ComicPlay());
        else if (_spriteIndex >= _sprites.Length && _autoPlay)
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

    void DialogueEnabled(bool isTrue)
    {
        if (!isTrue)
            StartCoroutine(FadeTextBox());
        else
        {
           _dialogueSquare.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            _dialogueSquare.SetActive(true);
        }
        _dialogueText.SetActive(isTrue);
        _dialogueSprite.SetActive(isTrue);
    }

    protected IEnumerator FadeTextBox(int fadeSpeed = 2)
    {
        Color color = _dialogueSquare.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (_dialogueSquare.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _dialogueSquare.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        _dialogueSquare.SetActive(false);
        yield return null;
    }

}
