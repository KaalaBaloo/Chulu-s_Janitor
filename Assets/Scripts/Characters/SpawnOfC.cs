using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class SpawnOfC : Enemy
{
    private void Update()
    {      

        MovementCondition();

    }

    void MovementCondition()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }

        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _moving = true;
            if (((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == _gridController.GetMovValue()
                        && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
            ||
                        (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == _gridController.GetMovValue())
                        && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x)))
            {
                _character.SubstractLife(_damage);
                StartCoroutine(AttackCoroutine(_rb, new Vector2(Mathf.RoundToInt(_character.transform.position.x) * _characterMovements,
                    Mathf.RoundToInt(_character.transform.position.y) * _characterMovements)));
            }
            else if (_gridController.GetPlayerVertical(_tileNumX) || _gridController.GetPlayerHorizontal(_tileNumY) || _gridController.GetPlayerNear(_tileNumX, _tileNumY))
            {
                //Debug.Log("In sight");
                SimpleMovement();
            }
            else
            {
                //Debug.Log("Not in sight");
                SetChangeTurn();
            }
        }
    }

    void SimpleMovement()
    {
        if (_gridController.GetPlayerVertical(_tileNumX) && GetCanMove(0, 1) && transform.position.y < _character.transform.position.y)
        {
                Movement(0, 1);
        }
        else if (_gridController.GetPlayerVertical(_tileNumX) && GetCanMove(0, -1) && transform.position.y > _character.transform.position.y)
        {
                Movement(0, -1);
        }
        else if (_gridController.GetPlayerHorizontal(_tileNumY) && GetCanMove(1, 0) && transform.position.x <  _character.transform.position.x)
        {
                Movement(1, 0);
        }
        else if (_gridController.GetPlayerHorizontal(_tileNumY) && GetCanMove(-1, 0) && transform.position.x > _character.transform.position.x)
        {
                Movement(-1, 0);
        }
        else if (_gridController.GetPlayerNear(_tileNumX, _tileNumY))
        {
            if (GetCanMove(1, 0) && transform.position.x < _character.transform.position.x)
            {
                Movement(1, 0);
            }
            else if (GetCanMove(-1, 0) && transform.position.x > _character.transform.position.x)
            {
                Movement(-1, 0);
            }
            else if (GetCanMove(0, 1) && transform.position.y < _character.transform.position.y)
            {
                Movement(0, 1);
            }
            else if (GetCanMove(0, -1) && transform.position.y > _character.transform.position.y)
            {
                Movement(0, -1);
            }
            else
            {
                //Debug.Log("Bloqueado");
                SetChangeTurn();
            }
        }
        else
        {
            //Debug.Log("Bloqueado");
            SetChangeTurn();
        }
    }

}
