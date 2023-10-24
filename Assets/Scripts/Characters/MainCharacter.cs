using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MainCharacter : Sprites 
{ 
    Rigidbody2D _rb;
    bool _destroyDirt = false;
    [SerializeField] int _lives = 1;
    int _movValue = 0;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _spriteNumber = 1;
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        _movValue = _gridController.GetMovValue();
    }

    void Update()
    {
        if (_gridController.GetTurn() == 1)
        {
            Movement();
            Clean();
        }
        GameOver();
    }

    //Calcula si es posible moverse a la celda elegida y, de serlo, mueve al personaje y cambia los datos de la grid y el turno
    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W) && _gridController.CanMove(_tileNumX, _tileNumY + 1))
        {
            _gridController.ChangeTurn(2);
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _tileNumY += 1;
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, _movValue * _characterMovements))); //Up
        }
        if (Input.GetKeyDown(KeyCode.A) && _gridController.CanMove(_tileNumX - 1, _tileNumY))
        {
            _gridController.ChangeTurn(2);
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _tileNumX -= 1;
            StartCoroutine(PositionCoroutine(_rb, new Vector2(-_movValue * _characterMovements, 0))); //Left
        }
        if (Input.GetKeyDown(KeyCode.S) && _gridController.CanMove(_tileNumX, _tileNumY - 1))  
        {
            _gridController.ChangeTurn(2);
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _tileNumY -= 1;
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -_movValue * _characterMovements))); //Down
        }
        if (Input.GetKeyDown(KeyCode.D) && _gridController.CanMove(_tileNumX + 1, _tileNumY))
        {
            _gridController.ChangeTurn(2);
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _tileNumX += 1;
            StartCoroutine(PositionCoroutine(_rb, new Vector2(_movValue * _characterMovements, 0))); //Right
        }
    }

    //Calcula si es posible limpiar la celda sobre la que está y, de serlo, manda la señal de eliminar sprite y cambia los datos de la grid y el turno
    private void Clean()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _gridController.CanClean(_tileNumX, _tileNumY))
        {
            _gridController.ChangeTurn(2);
            _gridController.SetDirtOnGrid(0, _tileNumX, _tileNumY);
            _gridController.DirtCleaned();
            _destroyDirt = true;
        }
    }

    //Elimina el sprite de "basura" sobre el que está el personaje
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_destroyDirt && collision.CompareTag("Dirt"))
        {
            _destroyDirt = false;
            Destroy(collision.gameObject);
            _gridController.ChangeTurn(0);
        }
    }

    public int GetX()
    {
        return _tileNumX;
    }

    public int GetY()
    {
        return _tileNumY;
    }

    public int GetLives()
    {
        return _lives;
    }

    public void SubstractLife(int damage)
    {
        _lives -= damage;
    }

    private void GameOver()
    {
        if (_lives <= 0)
        {
            Destroy(gameObject);
        }
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
        _gridController.ChangeTurn(0);
        yield return 0;
    }


    public void Push(int x, int y)
    {
        _gridController.SetGrid(0, _tileNumX, _tileNumY);
        _tileNumX += x;
        _tileNumY += y;
        StartCoroutine(PushCoroutine(_rb, new Vector2(_movValue * _characterMovements * x, _movValue * _characterMovements * y)));
    }

    protected IEnumerator PushCoroutine(Rigidbody2D rb, Vector2 position)
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
        yield return 0;
    }

}
