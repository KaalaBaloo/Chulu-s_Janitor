using JetBrains.Annotations;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Sprites
{
    Rigidbody2D _rb;
    MainCharacter _character;
    CustomAIPathFinding _customAIPathFinding;
    [SerializeField] int _damage = 1;

    bool _moving = false;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _character = GameObject.FindWithTag("MainCharacter").GetComponent<MainCharacter>();
        _customAIPathFinding = GetComponent<CustomAIPathFinding>();
    }

    void Start()
    {
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        _spriteNumber = 3;
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
    }

    private void Update()
    {
        if (_gridController.GetTurn() && _moving)
        {
            _moving = false;
        }
        if (!_gridController.GetTurn() && _character != null && !_moving)
        {
            _moving = true;
            if (_customAIPathFinding.GetReachEndPath() 
                && ((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == _gridController.GetMovValue()
                        && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
                    ||  
                        (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == _gridController.GetMovValue())
                        && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x))
                && (!_gridController.GetTurn()))
            {
                Debug.Log("damage");
                _character.SubstractLife(_damage);
                _rb.position = new Vector2(_character.transform.position.x, _character.transform.position.y);
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
        _gridController.ChangeTurn();
    }

    override protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 position)
    {
        Vector2 endingposition = new Vector2(Mathf.RoundToInt(rb.position.x + position.x), Mathf.RoundToInt(rb.position.y + position.y));
        Debug.Log("Posicion final: " + endingposition);
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
        _gridController.ChangeTurn();
        _gridController.SetGrid(3, _tileNumX, _tileNumY);
        yield return 0;
    }
}
