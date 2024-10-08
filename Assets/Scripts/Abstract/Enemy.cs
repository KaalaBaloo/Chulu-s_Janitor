using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public abstract class Enemy : Sprites
{
    protected Rigidbody2D _rb;
    protected MainCharacter _character;
    [SerializeField] protected bool _blockedView = true;
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected int _enemyNumber = 0;
    protected bool _patrol = false;
    protected bool _patrolDrch = false;
    protected int _patrolBlocked = 0;
    [SerializeField] int _patrolStage = 0;
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
                    PathFindingBlockView(_character.transform.position);
                }
                else
                {
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
        if (!_gridController.EnemyCanMove(x, y))
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
        Debug.Log("____");
        if (Mathf.Abs(character.x - transform.position.x) >= Mathf.Abs(character.y - transform.position.y))
        {
            if (character.x > transform.position.x)
            {
                Debug.Log("1");
                if (GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    Debug.Log("a");
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                }
                else if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    Debug.Log("b");
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (character.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    Debug.Log("c");
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    Debug.Log("d");
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    Debug.Log("e");
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    Debug.Log("f");
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    Debug.Log("g");
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                }
                else
                {
                    Debug.Log("Error in char.x > enemy.x");
                    SetChangeTurn();
                }
            }
            else if (character.x < transform.position.x)
            {
                Debug.Log("2");
                if (GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                }
                else if (character.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 1))
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
                    SetChangeTurn();
                }
            }
            else if (character.x == transform.position.x)
            {
                Debug.Log("3");
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
                    SetChangeTurn();
                }
            }
            else
            {
                Debug.Log("Error general PF");
                SetChangeTurn();
            }
        }
        else
        {
            if (character.x > transform.position.x)
            {
                Debug.Log("4");
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
                    SetChangeTurn();
                }
            }
            else if (character.x < transform.position.x)
            {
                Debug.Log("5");
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
                    SetChangeTurn();
                }
            }
            else if (character.x == transform.position.x)
            {
                Debug.Log("6");
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
                    SetChangeTurn();
                }
            }
            else
            {
                Debug.Log("Error general PF");
                SetChangeTurn();
            }
        }
    }

    protected void PathFindingBlockView(Vector3 character)
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
                SetChangeTurn();
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
                SetChangeTurn();
            }
        }
        else if (character.y > transform.position.y && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
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
                SetChangeTurn();
            }
        }
        else if (character.y < transform.position.y && _gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY))
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
                SetChangeTurn();
            }
        }
        else //Modo Patrulla
        {
            _patrol = true;
            if (!_patrolDrch)
            {
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
                else if (_patrolStage == 0 && GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 3;
                    _patrolDrch = true;
                }
                else if (_patrolStage == 3 && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                    _patrolStage = 2;
                    _patrolDrch = true;
                }
                else if (_patrolStage == 2 && GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 1;
                    _patrolDrch = true;
                }
                else if (_patrolStage == 1 && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 0;
                    _patrolDrch = true;
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY) && _patrolBlocked == 0)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 0;
                    _patrolBlocked = 1;
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY) && _patrolBlocked == 1)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 0;
                    _patrolBlocked = 0;
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1) && _patrolBlocked == 0)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                    _patrolStage = 0;
                    _patrolBlocked = 1;
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1) && _patrolBlocked == 1)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 0;
                    _patrolBlocked = 0;
                }
                else
                {
                    Debug.Log("Error patrol");
                    _patrolStage = 0;
                    _patrolBlocked = 0;
                    SetChangeTurn();
                }
            }
            else
            { 
                if (_patrolStage == 0 && GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 3;
                }
                else if (_patrolStage == 3 && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                    _patrolStage = 2;
                }
                else if (_patrolStage == 2 && GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 1;
                }
                else if (_patrolStage == 1 && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 0;
                }
                else if (_patrolStage == 0 && GetCanMove(_tileNumX - 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 1;
                    _patrolDrch = false;
                }
                else if (_patrolStage == 1 && GetCanMove(_tileNumX, _tileNumY - 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                    _patrolStage = 2;
                    _patrolDrch = false;
                }
                else if (_patrolStage == 2 && GetCanMove(_tileNumX + 1, _tileNumY))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 3;
                    _patrolDrch = false;
                }
                else if (_patrolStage == 3 && GetCanMove(_tileNumX, _tileNumY + 1))
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 0;
                    _patrolDrch = false;
                }
                else if (GetCanMove(_tileNumX - 1, _tileNumY) && _patrolBlocked == 0)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(-1, 0)));
                    _patrolStage = 0;
                    _patrolBlocked = 1;
                }
                else if (GetCanMove(_tileNumX + 1, _tileNumY) && _patrolBlocked == 1)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0)));
                    _patrolStage = 0;
                    _patrolBlocked = 0;
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 1) && _patrolBlocked == 0)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1)));
                    _patrolStage = 0;
                    _patrolBlocked = 1;
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 1) && _patrolBlocked == 1)
                {
                    StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1)));
                    _patrolStage = 0;
                    _patrolBlocked = 0;
                }
                else
                {
                    Debug.Log("Error patrol");
                    _patrolStage = 0;
                    SetChangeTurn();
                }
            }
            if (!_patrol)
            {
                _patrolStage = 0;
                _patrolBlocked = 0;
            }
        }
    }
}
