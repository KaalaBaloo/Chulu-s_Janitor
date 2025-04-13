using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private GameObject _text;
    [SerializeField] private AudioClip unlockedClip;
    [SerializeField] private AudioClip lockedClip;
    [SerializeField] private string[] _tittles;

    private GameObject _fadeBlack;
    private AudioSource _audio;
    private Animator _animator;
    private TMP_Text _textTittle;

    private int _level = 0;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = GeneralSettings.SFXVOLUME / 100f;
        _animator = _sprite.GetComponent<Animator>();
        _textTittle = _text.GetComponent<TMP_Text>();
        _fadeBlack = GameObject.FindWithTag("_blackFade");

        StartCoroutine(FadeFromBlack());

        for (int i = 0; i < _levels.Length; i++)
        {
            if (i <= GridController.LEVELS_UNLOCKED)
                _levels[i].GetComponent<LS_Buttons>().Available();
        }

        _level = GridController.LEVELS_UNLOCKED;
        UpdateSelectorPosition();
    }

    private void Update()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            GameObject level = _levels[i];
            Vector3 screenPos = Camera.main.WorldToScreenPoint(level.transform.position);
            Rect rect = new Rect(screenPos.x - 50, screenPos.y - 50, 100, 100); // Approx area

            if (rect.Contains(Input.mousePosition))
            {
                if (_level != i)
                {
                    _level = i;
                    UpdateSelectorPosition();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    TryLoadLevel();
                }

                break;
            }
        }
    }

    private void ChangeLevel(int direction)
    {
        _level += direction;
        UpdateSelectorPosition();
    }

    private void UpdateSelectorPosition()
    {
        Vector3 offset = new Vector3(-0.12f, 0.28f, 0f);
        transform.position = _levels[_level].transform.position + offset;
        _textTittle.text = _tittles[_level];
    }

    private void TryLoadLevel()
    {
        if (_level <= GridController.LEVELS_UNLOCKED)
        {
            _animator.SetTrigger("Clean");
            PlaySound(unlockedClip);
            StartCoroutine(FadeToBlack((_level + 1).ToString()));
        }
        else
        {
            PlaySound(lockedClip);
            Debug.Log("Blocked level");
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && !GeneralSettings.MUTED)
        {
            _audio.PlayOneShot(clip, GeneralSettings.SFXVOLUME / 100f);
        }
    }

    private IEnumerator FadeToBlack(string scene, int fadeSpeed = 5)
    {
        SpriteRenderer sr = _fadeBlack.GetComponent<SpriteRenderer>();
        Color color = sr.color;

        while (sr.color.a < 1f)
        {
            color.a += fadeSpeed * Time.deltaTime;
            sr.color = color;
            yield return null;
        }

        SceneManager.LoadScene(scene);
    }

    private IEnumerator FadeFromBlack(int fadeSpeed = 8)
    {
        SpriteRenderer sr = _fadeBlack.GetComponent<SpriteRenderer>();
        Color color = sr.color;

        while (sr.color.a > 0f)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            sr.color = color;
            yield return null;
        }
    }
}
