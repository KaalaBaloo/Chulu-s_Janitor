using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Spikes : Sprites
{
    bool _up = false;
    bool _playerTurn = true;
    MainCharacter _character;
    SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _spriteNumber = 2;
        _gridController.SetInteractive(_spriteNumber, _tileNumX, _tileNumY);
        _character = GameObject.FindWithTag("MainCharacter").GetComponent<MainCharacter>();
    }

    private void Update()
    {
        ChangeStatus();
        Damage();
    }

    void ChangeStatus()
    {
        if (_gridController.GetTurn() == 1 && _playerTurn)
        {
            _playerTurn = false;
            if (!_up)
            {
                _up = true;
                _renderer.color = Color.red;
            }
            else
            {
                _up = false;
                _renderer.color = Color.white;
            }
        }
        else if (_gridController.GetTurn() == 0 && !_playerTurn)
        {
            _playerTurn = true;
        }
    }

    void Damage()
    {
        if (_gridController.GetGridTile(_tileNumX, _tileNumY) == 1 && _up)
        {
            _character.GameOver();
            Debug.Log("GameOver");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
