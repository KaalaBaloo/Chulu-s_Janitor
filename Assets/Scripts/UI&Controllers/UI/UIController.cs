using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    GameObject _pause;
    GameObject _settings;

    private void Awake()
    {
        _settings = GameObject.FindWithTag("_settings");
        _pause = GameObject.FindWithTag("_pause");
    }

    private void Start()
    {
        if (_pause != null) _pause.SetActive(false);
    }

    void Update()
    {
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
        bool pauseActive = _pause != null && _pause.activeSelf;
        bool settingsActive = _settings != null && _settings.activeSelf;

        if (escapePressed && !pauseActive && !settingsActive)
        {
            Cursor.visible = true;
            if (_pause != null) _pause.SetActive(true);
        }
        else if (escapePressed && pauseActive && !settingsActive)
        {
            Continue();
        }
        else if (escapePressed && settingsActive)
        {
            Back();
        }
        else if (escapePressed)
        {
            Cursor.visible = false;
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
