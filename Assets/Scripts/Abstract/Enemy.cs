using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.ShaderKeywordFilter;

public abstract class Enemy : Sprites
{
    protected Rigidbody2D _rb;
    protected MainCharacter _character;
    [SerializeField] protected bool _blockedView = true;
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected int _enemyNumber = 0;
    protected bool _moving = false;
    protected bool _patrol = false;
    int _patrolStage = 0;
    protected Vector3 _characterLastTurn;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _character = GameObject.FindWithTag("MainCharacter").GetComponent<MainCharacter>();
    }

    protected virtual void Start()
    {
        _spriteNumber = 3;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        _gridController.CreateEnemy();
        _characterLastTurn = _character.transform.position;
    }

    virtual protected void MovePathFinding()
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
                    PathFindingBlockView(_character.transform.position);
                }
                else
                {
                    Debug.Log("PF");
                    PathFinding(_character.transform.position);
                }
            }
        }
    }

    protected virtual void Attack() { }

    //Se mueve en base al camino elegido
    public void Movement(int x, int y)
    {
        _gridController.SetGrid(0, _tileNumX, _tileNumY);
        StartCoroutine(PositionCoroutine(_rb, new Vector2(x * _characterMovements, y * _characterMovements)));
        _tileNumX += x;
        _tileNumY += y;
    }

    public bool GetCanMove(int x, int y)
    {
        if (!_gridController.CanMove(x, y))
        {
            return false;
        }
        else if (!_gridController.EnemyCanMove(x, y))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected virtual void SetChangeTurn()
    {
        _gridController.EnemyMoved();
    }

    override protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 position)
    {
        _gridController.SetGrid(0, _tileNumX, _tileNumY);
        _tileNumX += Mathf.RoundToInt(position.x);
        _tileNumY += Mathf.RoundToInt(position.y);
        Vector2 endingposition = new Vector2(Mathf.RoundToInt(rb.position.x + position.x), Mathf.RoundToInt(rb.position.y + position.y));
        if (transform.position.x > endingposition.x)
        {
            while (transform.position.x > endingposition.x)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.x < endingposition.x)
        {
            while (transform.position.x < endingposition.x)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.y > endingposition.y)
        {
            while (transform.position.y > endingposition.y)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else
        {
            while (transform.position.y < endingposition.y)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        rb.velocity = Vector2.zero;
        transform.position = endingposition;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        SetChangeTurn();
        yield return 0;
    }

    protected IEnumerator AttackCoroutine(Rigidbody2D rb, Vector2 position)
    {
        if (transform.position.x > position.x)
        {
            while (transform.position.x > position.x)
            {
                rb.velocity = (position - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.x < position.x)
        {
            while (transform.position.x < position.x)
            {
                rb.velocity = (position - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.y > position.y)
        {
            while (transform.position.y > position.y)
            {
                rb.velocity = (position - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else
        {
            while (transform.position.y < position.y)
            {
                rb.velocity = (position - rb.position).normalized * _speed;
                yield return null;
            }
        }
        rb.velocity = Vector2.zero;
        transform.position = position;
        _character.GameOver();
        _gridController.GameOver();
        yield return 0;
    }

    protected void PathFinding(Vector3 character)
    {
        if (Mathf.Abs(character.x - transform.position.x) > Mathf.Abs(character.y - transform.position.y))
        {
            if (character.x > transform.position.x)
            {
                if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x > enemy.x");
                }
            }
            else if (character.x < transform.position.x)
            {
                if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (character.y > transform.position.y && _gridController.CanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x < enemy.x");
                }
            }
            else if (character.x == transform.position.x)
            {
                if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x == enemy.x");
                }
            }
            else
            {
                Debug.Log("Error general PF");
            }
        }
        else
        {
            if (character.x > transform.position.x)
            {
                if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x > enemy.x");
                }
            }
            else if (character.x < transform.position.x)
            {
                if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }             
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x < enemy.x");
                }
            }
            else if (character.x == transform.position.x)
            {
                if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x == enemy.x");
                }
            }
            else
            {
                Debug.Log("Error general PF");
            }
        }
    }

    protected void PathFindingBlockView(Vector3 character)
    {
        if (Mathf.Abs(character.x - transform.position.x) > Mathf.Abs(character.y - transform.position.y))
        {
            if (character.x > transform.position.x && _gridController.GetPlayerHorizontalNoObstacle(_tileNumX, _tileNumY))
            {
                _patrol = false;
                if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x > enemy.x");
                }
            }
            else if (character.x < transform.position.x && _gridController.GetPlayerHorizontalNoObstacle(_tileNumX, _tileNumY))
            {
                _patrol = false;
                if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x < enemy.x");
                }
            }
            else if (character.x == transform.position.x && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
            {
                _patrol = false;
                if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x == enemy.x");
                }
            }
            else
            {
                _patrol = true;
                if (_patrolStage == 0 && GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 1;
                }
                else if (_patrolStage == 1 && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                    _patrolStage = 2;
                }
                else if (_patrolStage == 2 && GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 3;
                }
                else if (_patrolStage == 3 && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 0;
                }
                else if (_patrolStage == 0 && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 3;
                }
                else if (_patrolStage == 3 && GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 2;
                }
                else if (_patrolStage == 2 && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 1;
                }
                else if (_patrolStage == 1 && GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 0;
                }
                else
                {
                    Debug.Log("Error patrol");
                }

                if (!_patrol)
                {
                    _patrolStage = 0;
                }
            }
        }
        else // y > x
        {
            if (character.x > transform.position.x && _gridController.GetPlayerHorizontalNoObstacle(_tileNumX, _tileNumY))
            {
                _patrol = false;
                if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x > enemy.x");
                }
            }
            else if (character.x < transform.position.x && _gridController.GetPlayerHorizontalNoObstacle(_tileNumX, _tileNumY))
            {
                _patrol = false;
                if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1) && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x < enemy.x");
                }
            }
            else if (character.x == transform.position.x && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
            {
                _patrol = false;
                if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x == enemy.x");
                }
            }
            else
            {
                _patrol = true;
                if (_patrolStage == 0 && GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 1;
                }
                else if (_patrolStage == 1 && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                    _patrolStage = 2;
                }
                else if (_patrolStage == 2 && GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 3;
                }
                else if (_patrolStage == 3 && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 0;
                }
                else if (_patrolStage == 0 && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 3;
                }
                else if (_patrolStage == 3 && GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 2;
                }
                else if (_patrolStage == 2 && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 1;
                }
                else if (_patrolStage == 1 && GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 0;
                }
                else
                {
                    Debug.Log("Error patrol");
                }

                if (!_patrol)
                {
                    _patrolStage = 0;
                }
            }
        }         
    }
}
