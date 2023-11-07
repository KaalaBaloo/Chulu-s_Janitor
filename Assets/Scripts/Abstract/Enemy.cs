using JetBrains.Annotations;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Sprites
{
    protected Rigidbody2D _rb;
    protected MainCharacter _character;
    protected CustomAIPathFinding _customAIPathFinding;
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected int _enemyNumber = 0;

    protected bool _moving = false;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _character = GameObject.FindWithTag("MainCharacter").GetComponent<MainCharacter>();
        _customAIPathFinding = GetComponent<CustomAIPathFinding>();
    }

    void Start()
    {
        _spriteNumber = 3;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        _gridController .CreateEnemy();
    }

    protected void MovePathFinding()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _moving = true;

            if (_customAIPathFinding.GetReachEndPath()
            && ((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == _gridController.GetMovValue()
                    && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
        ||
                    (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == _gridController.GetMovValue())
                    && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x)))
            {
                _character.SubstractLife(_damage);
                StartCoroutine(AttackCoroutine(_rb, new Vector2(Mathf.RoundToInt(_character.transform.position.x) * _characterMovements,
                     Mathf.RoundToInt(_character.transform.position.y) * _characterMovements)));
            }
            else
            {
                _customAIPathFinding.SearchPath();
            }

        }
    }

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
        return _gridController.CanMove(_tileNumX + x, _tileNumY + y);
    }

    public void SetChangeTurn()
    {
        _gridController.EnemyMoved();
    }

    override protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 position)
    {
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
        Debug.Log("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield return 0;
    }

}
