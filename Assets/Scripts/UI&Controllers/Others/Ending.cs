using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] GameObject _credits;
    [SerializeField] float _creditsVel = 0.05f;
    [SerializeField] Sprite[] _creditsTexture;

    private float t = 0;
    private GameObject _fadeBlack;
    private LanguageManager _languageManager;
    private SpriteRenderer _creditsImage;


    private void Awake()
    {
        _fadeBlack = GameObject.FindWithTag("_blackFade");
        _languageManager = new LanguageManager();
        SetCreditsLanguage();
    }

    private void SetCreditsLanguage()
    {
        string[] credits = _languageManager.GetCreditsTexts();
        _credits.GetComponent<TMPro.TMP_Text>().text = string.Join("\n", credits);
        _creditsImage = _credits.GetComponentInChildren<SpriteRenderer>();
        _creditsImage.sprite = _creditsTexture[GeneralSettings.LANGUAGE];
        _creditsImage.enabled = false;
    }

    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(FadefromBlack());
    }

    virtual protected IEnumerator EndingCoroutine()
    {
        while (t < 20.75)
        {
            _credits.transform.position += new Vector3(0, _creditsVel, 0) * Time.deltaTime;
            t += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FadetoBlack("Main"));
        yield return 0;
    }

    protected IEnumerator FadetoBlack(string scene, int fadeSpeed = 8)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;
        float time = 0;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        _creditsImage.enabled = true;

        while (time < 3)
        {
            time += Time.deltaTime;
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

}
