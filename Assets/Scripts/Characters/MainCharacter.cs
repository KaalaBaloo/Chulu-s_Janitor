using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MainCharacter : Sprites 
{ 
    Rigidbody2D _rb;
    Collider2D _coll;
    int _characterMovements = 1;
    bool _destroyDirt = false;
    [SerializeField] int _lives = 1;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
    }

    void Start()
    {
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        _spriteNumber = 1;
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
    }

    void Update()
    {
        if (_gridController.GetTurn())
        {
            Movement(_gridController.GetMovValue());
            Clean();
        }
        GameOver();
    }

    //Calcula si es posible moverse a la celda elegida y, de serlo, mueve al personaje y cambia los datos de la grid y el turno
    private void Movement(int movValue)
    {
        if (Input.GetKeyDown(KeyCode.W) && _gridController.CanMove(_tileNumX, _tileNumY + 1))
        {
            _coll.isTrigger = true;
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _rb.MovePosition(new Vector2(transform.position.x, transform.position.y + movValue * _characterMovements)); //Up
            _tileNumY += 1;
            _gridController.SetGrid(1, _tileNumX, _tileNumY);
            _gridController.ChangeTurn();
            _coll.isTrigger = false;
        }
        if (Input.GetKeyDown(KeyCode.A) && _gridController.CanMove(_tileNumX - 1, _tileNumY))
        {
            _coll.isTrigger = true;
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _rb.position += new Vector2(-movValue * _characterMovements, 0); //Left
            _tileNumX -= 1;
            _gridController.SetGrid(1, _tileNumX, _tileNumY);
            _gridController.ChangeTurn();
            _coll.isTrigger = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && _gridController.CanMove(_tileNumX, _tileNumY - 1))  
        {
            _coll.isTrigger = true;
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _rb.position += new Vector2(0, -movValue * _characterMovements); //Down
            _tileNumY -= 1;
            _gridController.SetGrid(1, _tileNumX, _tileNumY);
            _gridController.ChangeTurn();
            _coll.isTrigger = false;
        }
        if (Input.GetKeyDown(KeyCode.D) && _gridController.CanMove(_tileNumX + 1, _tileNumY))
        {
            _coll.isTrigger = true;
            _gridController.SetGrid(0, _tileNumX, _tileNumY);
            _rb.position += new Vector2(movValue * _characterMovements, 0); //Right
            _tileNumX += 1;
            _gridController.SetGrid(1, _tileNumX, _tileNumY);
            _gridController.ChangeTurn();
            _coll.isTrigger = false;
        }
    }

    //Calcula si es posible limpiar la celda sobre la que está y, de serlo, manda la señal de eliminar sprite y cambia los datos de la grid y el turno
    private void Clean()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _gridController.CanClean(_tileNumX, _tileNumY))
        {
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
            Debug.Log("Yas");
            _destroyDirt = false;
            Destroy(collision.gameObject);
            _gridController.ChangeTurn();
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
}
