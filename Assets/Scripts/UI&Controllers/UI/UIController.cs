using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    GameObject _pause;
    GameObject _settings;

    void Start()
    {
        _pause = GameObject.FindWithTag("_pause");
        _pause.SetActive(false);
        _settings = GameObject.FindWithTag("_settings");
        _settings.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            _pause.SetActive(true);
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
