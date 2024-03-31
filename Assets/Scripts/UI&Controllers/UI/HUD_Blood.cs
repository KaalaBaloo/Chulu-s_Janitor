using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Blood : MonoBehaviour
{
    GameObject _player;
    MainCharacter _mainCharacter;
    Animator _animator;
    int _lastBlood = 0;


    void Start()
    {
        _player = GameObject.FindWithTag("MainCharacter");
        _mainCharacter = _player.GetComponent<MainCharacter>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(_lastBlood != _mainCharacter.GetSuciedad())
        {
            _lastBlood = _mainCharacter.GetSuciedad();
            if(_lastBlood == 0)
            {
                _animator.SetTrigger("0");
            }
            else if (_lastBlood == 1)
            {
                _animator.SetTrigger("1");
            }
            else if (_lastBlood == 2)
            {
                _animator.SetTrigger("2");
            }
            else if (_lastBlood == 3)
            {
                _animator.SetTrigger("3");
            }
            else
            {
                Debug.Log("Error Hud");
            }
        }
    }
}
