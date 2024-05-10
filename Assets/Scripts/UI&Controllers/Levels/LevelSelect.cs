using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    GameObject _fadeBlack;
    AudioSource _audio;
    [SerializeField] GameObject[] _levels;
    int _level = 0;
    [SerializeField] GameObject _sprite;
    Animator _animator;
    [SerializeField] GameObject _text;
    [SerializeField] string[] _tittles;
    TMP_Text _textTittle;


    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
        transform.position = new Vector3(_levels[0].transform.position.x - 0.12f, _levels[0].transform.position.y + 0.28f, 0);
        _animator = _sprite.GetComponent<Animator>();
        _textTittle = _text.GetComponent<TMP_Text>();
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        StartCoroutine(FadefromBlack());

        for (int i = 0; i < _levels.Length; i++)
        {
            if (i <= GridController.LEVELS_UNLOCKED)
                _levels[i].GetComponent<LS_Buttons>().Available();
        }

        _level = GridController.LEVELS_UNLOCKED;
        transform.position = new Vector3(_levels[_level].transform.position.x - 0.12f, _levels[_level].transform.position.y + 0.28f, 0);
        _textTittle.text = _tittles[_level];

        Debug.Log(GridController.LEVELS_UNLOCKED);
    }


    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && (_level + 1) < _levels.Length)
        {
            _level += 1;
            transform.position = new Vector3(_levels[_level].transform.position.x - 0.12f, _levels[_level].transform.position.y + 0.28f, 0);
            _textTittle.text = _tittles[_level];
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && (_level - 1) >= 0)
        {
            _level -= 1;
            transform.position = new Vector3(_levels[_level].transform.position.x - 0.12f, _levels[_level].transform.position.y + 0.28f, 0);
            _textTittle.text = _tittles[_level];
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && (_level - 1) >= 0)
        {
            _level -= 1;
            transform.position = new Vector3(_levels[_level].transform.position.x - 0.12f, _levels[_level].transform.position.y + 0.28f, 0);
            _textTittle.text = _tittles[_level];
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && (_level + 1) < _levels.Length)
        {
            _level += 1;
            transform.position = new Vector3(_levels[_level].transform.position.x - 0.12f, _levels[_level].transform.position.y + 0.28f, 0);
            _textTittle.text = _tittles[_level];
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_level <= GridController.LEVELS_UNLOCKED)
            {
                _animator.SetTrigger("Clean");
                _audio.volume = GeneralSettings.SFXVOLUME / 100;
                if (!GeneralSettings.MUTED)
                {
                    _audio.Play();
                }
                StartCoroutine(FadetoBlack((_level + 1).ToString()));
            }
            else
            {
                Debug.Log("Block");
            }  
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(FadetoBlack("Main"));
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
