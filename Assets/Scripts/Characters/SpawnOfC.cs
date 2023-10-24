using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpawnOfC : Enemy
{
    private void Update()
    {      

        MovementCondition();

    }

    void MovementCondition()
    {
        if (_gridController.GetTurn() == 1 && _moving)
        {
            _moving = false;
        }

        if (_gridController.GetTurn() == 0 && _character != null && !_moving)
        {
            _moving = true;
            if (((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == _gridController.GetMovValue()
                        && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
            ||
                        (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == _gridController.GetMovValue())
                        && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x)))
            {
                Debug.Log("damage");
                _character.SubstractLife(_damage);
                _rb.position = new Vector2(_character.transform.position.x, _character.transform.position.y);
            }
            else if (_gridController.GetPlayerVertical(_tileNumX) || _gridController.GetPlayerHorizontal(_tileNumY))
            {
                Debug.Log("In sight");
                _gridController.ChangeTurn(2);
                SimpleMovement();
            }
            else
            {
                Debug.Log("Not in sight");
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
        else
        {
            Debug.Log("Bloqueado");
            SetChangeTurn();
        }
    }

}
