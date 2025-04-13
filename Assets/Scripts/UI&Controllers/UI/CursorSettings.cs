using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorSettings : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private AudioSource _audio;
    private bool _cursorVisible;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "LevelSelector")
            _cursorVisible = true;
        else
            _cursorVisible = false;
        Cursor.visible = _cursorVisible;

        _audio = GetComponent<AudioSource>();
        UpdateVolume();
    }

    private void Update()
    {
        UpdateVolume();

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

            if (_cursorVisible && !GeneralSettings.MUTED)
            {
                _audio.PlayOneShot(_audio.clip);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    private void UpdateVolume()
    {
        _audio.volume = GeneralSettings.SFXVOLUME / 100f;
    }
}
