using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class aMigo : Enemy
{

    private void Update()
    {
        if (!GridController.GAMEOVER)
            MovePathFinding();

    }

    protected override void MovePathFinding()
    {
        if (_gridController.GetTurn() == 0)
        {
            _characterLastTurn = _character.transform.position;
        }
        if (_gridController.GetTurn() == 1 && _character != null && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _gridController.ChangeTurn(2);
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
        }
    }

    protected override void Attack()
    {
        Debug.Log("Push");
        if (_character.transform.position.x > transform.position.x && GetCanMove(_tileNumX + 2, _tileNumY))
        {
            Debug.Log("1");
            _character.Push(1, 0);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
        }
        else if (_character.transform.position.x < transform.position.x && GetCanMove(_tileNumX - 2, _tileNumY))
        {
            Debug.Log("2");
            _character.Push(-1, 0);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
        }
        else if (_character.transform.position.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 2))
        {
            Debug.Log("3");
            _character.Push(0, -1);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));

        }
        else if (_character.transform.position.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 2))
        {
            Debug.Log("4");
            _character.Push(0, 1);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
        }
        else if (_character.transform.position.x > transform.position.x && !GetCanMove(_tileNumX + 2, _tileNumY))
        {
            if (GetCanMove(_tileNumX - 1, _tileNumY))
            {
                Debug.Log("2");
                _character.Push(-1, 0);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
            }
            else if (GetCanMove(_tileNumX, _tileNumY - 2) && GetCanMove(_tileNumX + 1, _tileNumY - 1))
            {
                Debug.Log("3");
                _character.Push(0, -1);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));

            }
            else if (GetCanMove(_tileNumX, _tileNumY + 2) && GetCanMove(_tileNumX + 1, _tileNumY + 1))
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
        else if (_character.transform.position.x < transform.position.x && !GetCanMove(_tileNumX - 2, _tileNumY))
        {
            if (GetCanMove(_tileNumX + 1, _tileNumY))
            {
                Debug.Log("1");
                _character.Push(1, 0);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
            }
            else if (GetCanMove(_tileNumX, _tileNumY - 2) && GetCanMove(_tileNumX - 1, _tileNumY - 1))
            {
                Debug.Log("3");
                _character.Push(0, -1);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));

            }
            else if (GetCanMove(_tileNumX, _tileNumY + 2) && GetCanMove(_tileNumX - 1, _tileNumY + 1))
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
        else if (_character.transform.position.y < transform.position.y && !GetCanMove(_tileNumX, _tileNumY - 2))
        {
            if (GetCanMove(_tileNumX, _tileNumY + 1))
            {
                Debug.Log("4");
                _character.Push(0, 1);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
            }
            else if (GetCanMove(_tileNumX - 1, _tileNumY) && GetCanMove(_tileNumX - 1, _tileNumY - 1))
            {
                Debug.Log("1");
                _character.Push(-1, 0);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
            }
            else if (GetCanMove(_tileNumX + 1, _tileNumY) && GetCanMove(_tileNumX + 1, _tileNumY - 1))
            {
                Debug.Log("2");
                _character.Push(1, 0);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
            }
            else
            {
                Debug.Log("5");
                SetChangeTurn();
            }
        }
        else if (_character.transform.position.y > transform.position.y && !GetCanMove(_tileNumX, _tileNumY + 2))
        {
            if (GetCanMove(_tileNumX, _tileNumY - 1))
            {
                Debug.Log("4");
                _character.Push(0, -1);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
            }
            else if (GetCanMove(_tileNumX + 1, _tileNumY) && GetCanMove(_tileNumX + 1, _tileNumY + 1))
            {
                Debug.Log("1");
                _character.Push(1, 0);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
            }
            else if (GetCanMove(_tileNumX - 1, _tileNumY) && GetCanMove(_tileNumX - 1, _tileNumY + 1))
            {
                Debug.Log("2");
                _character.Push(-1, 0);
                StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
            }
            else
            {
                Debug.Log("5");
                SetChangeTurn();
            }
        }
        else
        {
            Debug.Log("5");
            SetChangeTurn();
        }
    }
}
