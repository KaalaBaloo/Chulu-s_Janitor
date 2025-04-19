using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using static LanguageData;

public class ComicController : MonoBehaviour
{
    [Header("Comic Settings")]
    [SerializeField] Sprite[] _comicSprites;
    [SerializeField] int _comicIndex = 0;
    [SerializeField] int[] _changeAuto;

    [Header("Dialogue Settings")]
    [SerializeField] int _dialogueScene;
    [SerializeField] Color[] _textColors;

    [Header("Other Settings")]
    [SerializeField] string _nextScene;
    [SerializeField] int[] _fadesBlack;
    [SerializeField] int _chuluVFXCamShake;
    [SerializeField] GameObject _VFXDead;
    [SerializeField] AudioSource _audioChulu;

    private bool _autoPlay = true;
    private bool _clickDisabled = false;
    private bool _clickComic = false;
    private int _fadesIndex = 0;
    private int _changeIndex = 0;
    private GameObject _fadeBlack;
    private DialogueManager _dialogueManager;
    private Image _comicImage;

    private int _dialogueIndex = 0;
    private string[] _dialogues;
    private TMP_Text _text;
    private Sprite[] _dialogueImages;
    private GameObject _dialogueSquare;
    private GameObject _dialogueText;
    private GameObject _dialogueSprite;
    private Image _dialogueImage;


    private void Awake()
    {
        FindElements();

        _dialogueManager = new DialogueManager();
        _dialogues = _dialogueManager.GetDialogueTexts(_dialogueScene);
        _dialogueImages = _dialogueManager.GetDialogueSprites(_dialogueScene);

        _audioChulu.clip = new SoundManager().GetCharacterSFX("Chulu", "main");
    }

    private void FindElements()
    {
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        _comicImage = GameObject.FindGameObjectWithTag("_comic").GetComponent<Image>();
        _dialogueSquare = GameObject.FindWithTag("_dialogue");
        _dialogueText = GameObject.FindWithTag("_dialogueText");
        _text = _dialogueText.GetComponent<TMP_Text>();
        _dialogueSprite = GameObject.FindGameObjectWithTag("_dialogueImage");
        _dialogueImage = _dialogueSprite.GetComponent<Image>();
    }

    void Start()
    {
        _clickDisabled = true;
        _comicIndex = 0;
        _text.text = _dialogues[_dialogueIndex];
        _text.color = _textColors[_dialogueIndex];
        _dialogueImage.sprite = _dialogueImages[_dialogueIndex];

        if (_comicSprites.Length > 0)
        {
            _comicImage.sprite = _comicSprites[_comicIndex];
            _dialogueSquare.SetActive(false);
            _dialogueText.SetActive(false);
            _dialogueSprite.SetActive(false);
        }
        else
        {
            _autoPlay = false;
            _dialogueSquare.SetActive(true);
            _dialogueText.SetActive(true);
            _dialogueSprite.SetActive(true);
        }

        StartCoroutine(FadefromBlack());

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Skip();
        }

        if (!_autoPlay && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && (_comicIndex < _comicSprites.Length || _dialogueIndex < _dialogues.Length) && !_clickDisabled)
        {
            if (_chuluVFXCamShake - 1 != _comicIndex)
                _dialogueIndex++;
            StartCoroutine(ComicPlay());
        }

        else if (!_autoPlay && _comicIndex >= _comicSprites.Length && _dialogueIndex >= _dialogues.Length && !_clickDisabled)
        {
            _autoPlay = true;
            StartCoroutine(FadetoBlackScene(_nextScene));
        }

        else if (_autoPlay && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && (_comicIndex < _comicSprites.Length || _dialogueIndex < _dialogues.Length))
        {
            _clickComic = true;
        }
    }

    public void Skip()
    {
        StartCoroutine(FadetoBlackScene(_nextScene));
    }

    protected IEnumerator ComicPlay()
    {
        _clickDisabled = true;
        float t = 0;
        int v = 4;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            yield return null;
        }

        _comicIndex++;

        if(_changeAuto.Length >= _changeIndex + 1)
        {
            if (_comicIndex == _changeAuto[_changeIndex])
            {
                _changeIndex++;
                _autoPlay = false;
                v = 8;
            }
            
        }

        else if (_changeAuto.Length < _changeIndex)
        {
            _autoPlay = false;
            v = 8;
        }

        else
        {
            _autoPlay = true;  
        }

        if(_autoPlay && !_clickComic)
        {
            while (t < 2f)
            {
                t += Time.deltaTime;

                if(_clickComic)
                {
                    _clickComic = false;
                    break;
                }

                yield return null;
            }
        }

        if (_chuluVFXCamShake == _comicIndex)
        {
            StartCoroutine(ChuluVFXCamShake());
        }
        else if (_fadesBlack.Length >= _fadesIndex + 1)
        {
            if (_fadesBlack[_fadesIndex] == _comicIndex)
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
        if (_comicIndex < _comicSprites.Length)
            _comicImage.sprite = _comicSprites[_comicIndex];

        if (_dialogueIndex < _dialogueImages.Length && !_autoPlay)
        {
            _text.text = _dialogues[_dialogueIndex];
            _text.color = _textColors[_dialogueIndex];
            _dialogueImage.sprite = _dialogueImages[_dialogueIndex];
            DialogueEnabled(true, true);
        }
        else
            DialogueEnabled(false, true);

        if (_clickComic)
            _clickComic = false;

        if (_comicIndex < _comicSprites.Length && _autoPlay)
            StartCoroutine(ComicPlay());
        else if (_comicIndex >= _comicSprites.Length && _autoPlay)
            StartCoroutine(FadetoBlackScene(_nextScene));

        yield return null;
    }
    void DialogueEnabled(bool isTrue, bool canClick)
    {
        Color color = _dialogueSquare.GetComponent<SpriteRenderer>().color;

        _dialogueSquare.SetActive(isTrue);
        _dialogueText.SetActive(isTrue);
        _dialogueSprite.SetActive(isTrue);

        if (!isTrue)
        {
            StartCoroutine(FadeTextBox());
        }
        else
        {
            _dialogueSquare.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (canClick)
            _clickDisabled = false;
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

    protected IEnumerator ChuluVFXCamShake()
    {
        float t = 0;
        Vector3 _initialPosition = _dialogueImage.transform.position;

        while (t < 1.2f)
        {
            _dialogueImage.transform.position = new Vector3(Mathf.Sin(Time.time * 20) * 10, 0, 0);
            t += Time.deltaTime;
            yield return null;
        }

        _dialogueImage.transform.position = _initialPosition;
        _audioChulu.Play();

        _dialogueIndex++;
        _dialogueImage.sprite = _dialogueImages[_dialogueIndex];
        _text.text = _dialogues[_dialogueIndex];
        _text.color = _textColors[_dialogueIndex];

        GameObject effect = Instantiate(_VFXDead, new Vector3(0, 0, 0), Quaternion.identity);
        effect.transform.localScale = new Vector3(5, 5, 0);
        GameObject effect2 = Instantiate(_VFXDead, new Vector3(0, 0, 0), Quaternion.identity);
        effect2.transform.localScale = new Vector3(5, 5, 0);

        t = 0;
        while (t < 2f)
        {
            t += Time.deltaTime;
            yield return null;
        }

        if (_comicIndex < _comicSprites.Length && _autoPlay)
            StartCoroutine(ComicPlay());
        else if (_comicIndex >= _comicSprites.Length && _autoPlay)
            StartCoroutine(FadetoBlackScene(_nextScene));

        _clickDisabled = false;

        yield return null;
    }

    protected IEnumerator FadetoBlack(int fadeSpeed = 4)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        _clickDisabled = true;
        if (_clickComic)
            fadeSpeed = 8;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        if (_comicIndex < _comicSprites.Length)
            _comicImage.sprite = _comicSprites[_comicIndex];

        if (_dialogueIndex < _dialogueImages.Length && !_autoPlay)
        {
            _text.text = _dialogues[_dialogueIndex];
            _text.color = _textColors[_dialogueIndex];
            _dialogueImage.sprite = _dialogueImages[_dialogueIndex];
            DialogueEnabled(true, false);
        }
        else
            DialogueEnabled(false, false);

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        if (_clickComic)
            _clickComic = false;

        if (_comicIndex < _comicSprites.Length && _autoPlay)
            StartCoroutine(ComicPlay());
        else if (_comicIndex >= _comicSprites.Length && _autoPlay)
            StartCoroutine(FadetoBlackScene(_nextScene));

        _clickDisabled = false;
        yield return null;
    }

    protected IEnumerator FadetoBlackScene(string scene, int fadeSpeed = 8)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;
        float t = 0;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        while (t < 1f)
        {
            t += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(scene);
        yield return null;
    }

    protected IEnumerator FadefromBlack(int fadeSpeed = 8)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;
        float t = 0;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        while(t<0.5f)
        {
            t += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(ComicPlay());

        _clickDisabled = false;
        yield return null;
    }
}
