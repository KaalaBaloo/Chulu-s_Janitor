using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class aMigo : Enemy
{
    private void Update()
    {
        PushPathFinding();

    }


    void PushPathFinding()
    {
        if (_gridController.GetTurn() == 1 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 0 && _character != null && !_moving)
        {
            _moving = true;

            if (_customAIPathFinding.GetReachEndPath()
            && ((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == _gridController.GetMovValue()
                    && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
        ||
                    (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == _gridController.GetMovValue())
                    && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x)))
            {
                Debug.Log("push");
                if (_character.transform.position.x > transform.position.x && _gridController.CanMove(2 + _tileNumX, _tileNumY))
                {
                    _character.Push(1, 0);
                    Movement(1, 0);
                }
                else if (_character.transform.position.x < transform.position.x && _gridController.CanMove(-2 + _tileNumX, _tileNumY))
                {
                    _character.Push(-1, 0);
                    Movement(-1, 0);
                }
                else if (_character.transform.position.y < transform.position.y && _gridController.CanMove(_tileNumX, -2 + _tileNumY))
                {
                    _character.Push(0, -1);
                    Movement(0, -1);
                    
                }
                else if(_character.transform.position.y > transform.position.y && _gridController.CanMove(_tileNumX, 2 + _tileNumY))
                {
                    _character.Push(0, 1);
                    Movement(0, 1);
                }
                else
                {
                    SetChangeTurn();
                }
            }
            else
            {
                _customAIPathFinding.SearchPath();
            }

        }
    }
}
