using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class aMigo : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        GetReferences();
    }

    protected override void Start()
    {
        base.Start();
        _move = new SoundManager().GetCharacterSFX("Amigo", "move");
    }

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
        if (_character.transform.position.x > transform.position.x && GetCanMove(_tileNumX + 2, _tileNumY))
        {
            Push(1, 0);
        }
        else if (_character.transform.position.x < transform.position.x && GetCanMove(_tileNumX - 2, _tileNumY))
        {
            Push(-1, 0);
        }
        else if (_character.transform.position.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 2))
        {
            Push(0, -1);

        }
        else if (_character.transform.position.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 2))
        {
            Push(0, 1);
        }
        else if (_character.transform.position.x > transform.position.x && !GetCanMove(_tileNumX + 2, _tileNumY))
        {
            if (GetCanMove(_tileNumX - 1, _tileNumY))
            {
                Push(-1, 0);
            }
            else if (GetCanMove(_tileNumX, _tileNumY - 2) && GetCanMove(_tileNumX + 1, _tileNumY - 1))
            {
                Push(0, -1);

            }
            else if (GetCanMove(_tileNumX, _tileNumY + 2) && GetCanMove(_tileNumX + 1, _tileNumY + 1))
            {
                Push(0, 1);
            }
            else 
            {
                SetChangeTurn();
            }
        }
        else if (_character.transform.position.x < transform.position.x && !GetCanMove(_tileNumX - 2, _tileNumY))
        {
            if (GetCanMove(_tileNumX + 1, _tileNumY))
            {
                Push(1, 0);
            }
            else if (GetCanMove(_tileNumX, _tileNumY - 2) && GetCanMove(_tileNumX - 1, _tileNumY - 1))
            {
                Push(0, -1);

            }
            else if (GetCanMove(_tileNumX, _tileNumY + 2) && GetCanMove(_tileNumX - 1, _tileNumY + 1))
            {
                Push(0, 1);
            }
            else
            {
                SetChangeTurn();
            }
        }
        else if (_character.transform.position.y < transform.position.y && !GetCanMove(_tileNumX, _tileNumY - 2))
        {
            if (GetCanMove(_tileNumX, _tileNumY + 1))
            {
                Push(0, 1);
            }
            else if (GetCanMove(_tileNumX - 1, _tileNumY) && GetCanMove(_tileNumX - 1, _tileNumY - 1))
            {
                Push(-1, 0);
            }
            else if (GetCanMove(_tileNumX + 1, _tileNumY) && GetCanMove(_tileNumX + 1, _tileNumY - 1))
            {
                Push(1, 0);
            }
            else
            {
                SetChangeTurn();
            }
        }
        else if (_character.transform.position.y > transform.position.y && !GetCanMove(_tileNumX, _tileNumY + 2))
        {
            if (GetCanMove(_tileNumX, _tileNumY - 1))
            {
                Push(0, -1);
            }
            else if (GetCanMove(_tileNumX + 1, _tileNumY) && GetCanMove(_tileNumX + 1, _tileNumY + 1))
            {
                Push(1, 0);
            }
            else if (GetCanMove(_tileNumX - 1, _tileNumY) && GetCanMove(_tileNumX - 1, _tileNumY + 1))
            {
                Push(-1, 0);
            }
            else
            {
                SetChangeTurn();
            }
        }
        else
        {
            SetChangeTurn();
        }
    }

    private void Push(int x, int y)
    {
        _audioSource.PlayOneShot(new SoundManager().GetCharacterSFX("Amigo", "attack"));
        _character.Push(x, y);
        StartCoroutine(PositionCoroutine(_rb, new Vector2(x, y)));
    }
}
