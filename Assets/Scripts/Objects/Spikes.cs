using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Spikes : Sprites
{
    bool _up = false;
    int _playerTurn = 1;
    MainCharacter _character;
    [SerializeField] GameObject _sprite;
    Animator _animator;
    AudioSource _audioSource;

    protected override void Awake()
    {
        _gameController = GameObject.FindWithTag("GameController");
        _gridController = _gameController.GetComponent<GridController>();
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
        _character = GameObject.FindWithTag("MainCharacter").GetComponent<MainCharacter>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        _spriteNumber = 2;
        _gridController.SetInteractive(_spriteNumber, _tileNumX, _tileNumY);
        _animator = _sprite.GetComponent<Animator>();
    }

    private void Update()
    {
        _audioSource.volume = GeneralSettings.SFXVOLUME / 100;
        ChangeStatus();
        Damage();
    }

    void ChangeStatus()
    {
        if (_gridController.GetTurn() == 1 && _character != null && _playerTurn == 1)
        {
            if (!_up)
            {
                _up = true;
                _animator.SetTrigger("Up");
                if (Random.value > 0.6f)
                {
                    _audioSource.PlayOneShot(SoundManager.Instance.GetCharacterSFX("Spikes", "out"));
                }
                _playerTurn = 0;
            }
            else
            {
                _up = false;
                _animator.SetTrigger("Down");
                if (Random.value > 0.6f)
                {
                    _audioSource.PlayOneShot(SoundManager.Instance.GetCharacterSFX("Spikes", "in"));
                }
                _playerTurn = 0;
            }
        }
        else if (_gridController.GetTurn() == 0 && _playerTurn == 0)
        {
            _playerTurn = 1;
        }
    }

    void Damage()
    {
        if (_gridController.GetGridTile(_tileNumX, _tileNumY) == 1 && _up && !GridController.GAMEOVER)
        {
            GridController.GAMEOVER = true;
            _character.GameOver();
            _gridController.GameOver();
        }
    }
}
