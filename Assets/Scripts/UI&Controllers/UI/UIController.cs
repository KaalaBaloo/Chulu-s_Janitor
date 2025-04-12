using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    GameObject _pause;
    GameObject _settings;
    GameObject _dialogues;

    void Start()
    {
        _pause = GameObject.FindWithTag("_pause");
        if (_pause != null) _pause.SetActive(false);

        _settings = GameObject.FindWithTag("_settings");
        if (_settings != null) _settings.SetActive(false);

        _dialogues = GameObject.FindWithTag("_dialogue");
    }

    void Update()
    {
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
        bool dialoguesActive = _dialogues != null && _dialogues.activeSelf;
        bool pauseActive = _pause != null && _pause.activeSelf;
        bool settingsActive = _settings != null && _settings.activeSelf;

        if (escapePressed && !dialoguesActive && !pauseActive && !settingsActive)
        {
            Cursor.visible = true;
            if (_pause != null) _pause.SetActive(true);
        }
        else if (escapePressed && !dialoguesActive && pauseActive && !settingsActive)
        {
            Continue();
        }
        else if (escapePressed && !dialoguesActive && settingsActive)
        {
            Back();
        }
    }

    public void Continue()
    {
        Cursor.visible = false;
        _pause.SetActive(false);
        _settings.SetActive(false);
    }

    public void Settings()
    {
        _settings.SetActive(true);
        _pause.SetActive(false);
    }

    public void Back()
    {
        _pause.SetActive(true);
        _settings.SetActive(false);
    }


    public bool GetPaused()
    {
        return _pause.activeSelf;
    }
}
