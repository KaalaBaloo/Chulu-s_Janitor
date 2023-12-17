using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class aMigo : Enemy
{

    private void Update()
    {
        MovePathFinding();

    }

    protected override void MovePathFinding()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _moving = true;
            if (((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == 1
                        && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
            ||
                        (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == 1)
                        && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x)))
            {
                Attack();
            }
            else
            {
                _moving = true;
                if (_blockedView)
                {
                    Debug.Log("PFBV");
                    PathFindingBlockView(_characterLastTurn);
                }
                else
                {
                    Debug.Log("PF");
                    PathFinding(_characterLastTurn);
                }
            }
            _characterLastTurn = _character.transform.position;
        }
    }

    protected override void Attack()
    {
        Debug.Log("Push");
        if (_character.transform.position.x > transform.position.x && _gridController.CanMove(_tileNumX + 2, _tileNumY))
        {
            Debug.Log("1");
            _character.Push(1, 0);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
        }
        else if (_character.transform.position.x < transform.position.x && _gridController.CanMove(_tileNumX - 2, _tileNumY))
        {
            Debug.Log("2");
            _character.Push(-1, 0);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
        }
        else if (_character.transform.position.y < transform.position.y && _gridController.CanMove(_tileNumX, _tileNumY - 2))
        {
            Debug.Log("3");
            _character.Push(0, -1);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));

        }
        else if (_character.transform.position.y > transform.position.y && _gridController.CanMove(_tileNumX, _tileNumY + 2))
        {
            Debug.Log("4");
            _character.Push(0, 1);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
        }
        else if (_gridController.CanMove(_tileNumX + 2, _tileNumY))
        {
            Debug.Log("1");
            _character.Push(1, 0);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
        }
        else if (_gridController.CanMove(_tileNumX - 2, _tileNumY))
        {
            Debug.Log("2");
            _character.Push(-1, 0);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
        }
        else if (_gridController.CanMove(_tileNumX, _tileNumY - 2))
        {
            Debug.Log("3");
            _character.Push(0, -1);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));

        }
        else if (_gridController.CanMove(_tileNumX, _tileNumY + 2))
        {
            Debug.Log("4");
            _character.Push(0, 1);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
        }
        else
        {
            Debug.Log("5");
            SetChangeTurn();
        }
    }
}
