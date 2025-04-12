using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GameObject _fadeBlack;
    private AudioSource _audio;

    private void Start()
    {
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        StartCoroutine(FadeFromBlack());

        _audio = GetComponent<AudioSource>();
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        _audio.volume = GeneralSettings.SFXVOLUME / 100f;
    }

    private void PlayClickSound()
    {
        UpdateVolume();
        if (!GeneralSettings.MUTED)
        {
            _audio.PlayOneShot(_audio.clip);
        }
    }

    public void LoadLevel(string levelName)
    {
        PlayClickSound();
        StartCoroutine(FadeToBlack(levelName));
    }

    public void LoadLevelByIndex(int index)
    {
        LoadLevel(index.ToString());
    }

    public void LoadLevelSelector()
    {
        LoadLevel("SelectorNiveles");
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
