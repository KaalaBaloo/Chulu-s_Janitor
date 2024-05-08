using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorSettings : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    AudioSource _audio;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main")
            Cursor.visible = true;
        _audio = GetComponent<AudioSource>();
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            _audio.volume = GeneralSettings.SFXVOLUME / 100;
            if (!GeneralSettings.MUTED)
            {
                _audio.Play();
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

}
