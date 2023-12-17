using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : Sprites
{
    bool _up = false;
    int _playerTurn = 1;
    MainCharacter _character;
    [SerializeField] GameObject _sprite;
    Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _character = GameObject.FindWithTag("MainCharacter").GetComponent<MainCharacter>();
    }

    private void Start()
    {
        _spriteNumber = 2;
        _gridController.SetInteractive(_spriteNumber, _tileNumX, _tileNumY);
        _animator = _sprite.GetComponent<Animator>();
    }

    private void Update()
    {
        ChangeStatus();
        Damage();
    }

    void ChangeStatus()
    {
        if (_gridController.GetTurn() == 1 && _character != null && _playerTurn == 1)
        {
            _playerTurn = 2;
            if (!_up)
            {
                _up = true;
                _animator.SetTrigger("Up");
            }
            else
            {
                _up = false;
                _animator.SetTrigger("Down");
            }
            _playerTurn = 0;
        }
        else if (_gridController.GetTurn() == 0 && _playerTurn == 0)
        {
            _playerTurn = 1;
        }
    }

    void Damage()
    {
        if (_gridController.GetGridTile(_tileNumX, _tileNumY) == 1 && _up)
        {
            _character.GameOver();
            _gridController.Restart();
            Debug.Log("GameOver");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
