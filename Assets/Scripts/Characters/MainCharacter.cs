using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class MainCharacter : Sprites 
{ 
    Rigidbody2D _rb;
    bool _destroyDirt = false;
    [SerializeField] int _lives = 1;
    [SerializeField] int _suciedad = 0;
    [SerializeField] GameObject _sprite;
    [SerializeField] GameObject _sangre;
    Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _animator = _sprite.GetComponent<Animator>();
    }

    void Start()
    {
        _spriteNumber = 1;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
    }

    void Update()
    {
        if (_gridController.GetTurn() == 0)
        {
            Movement();
        }
        Control();
    }

    //Calcula si es posible moverse a la celda elegida y, de serlo, mueve al personaje y cambia los datos de la grid y el turno
    private void Movement()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))&& _gridController.CanMove(_tileNumX, _tileNumY + 1))
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1))); //Up
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _gridController.CanMove(_tileNumX - 1, _tileNumY))
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(-1 , 0))); //Left
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _gridController.CanMove(_tileNumX, _tileNumY - 1))  
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1))); //Down
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _gridController.CanMove(_tileNumX + 1, _tileNumY))
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0))); //Right
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _gridController.GetTurn() == 0)
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(CleanCoroutine());
        }
    }

    //Salir / Reiniciar
    private void Control()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _gridController.Restart();
        }
    }

    //Elimina el sprite de "basura" sobre el que está el personaje
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_destroyDirt && collision.CompareTag("Dirt"))
        {
            _destroyDirt = false;
            Destroy(collision.gameObject); 
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

    public void GameOver()
    {
        Destroy(gameObject);
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
        _gridController.ChangeTurn(1);
        yield return 0;
    }

    protected IEnumerator PushCoroutine(Rigidbody2D rb, Vector2 position)
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
        yield return 0;
    }

    public void Push(int x, int y)
    {
        StartCoroutine(PushCoroutine(_rb, new Vector2(x, y)));
    }

    protected IEnumerator CleanCoroutine()
    {
        float t = 0;
        _animator.SetTrigger("Clean");
        while (t < 1)
        {
            t += Time.deltaTime;
            yield return null;
        }
        if (_gridController.CanClean(_tileNumX, _tileNumY) == 1 && _suciedad < 3)
        {
            _gridController.SetInteractive(0, _tileNumX, _tileNumY);
            _gridController.DirtCleaned();
            _destroyDirt = true;
            _suciedad++;
        }
        else if (_gridController.CanClean(_tileNumX, _tileNumY) == 2 && _suciedad < 3)
        {
            _gridController.SetInteractive(1, _tileNumX, _tileNumY);
            _gridController.DirtCleaned();
            Instantiate(_sangre, transform.position, Quaternion.identity);
            _destroyDirt = true;
            _suciedad++;
        }
        else if (_gridController.CanClean(_tileNumX, _tileNumY) == 3)
        {
            _suciedad = 0;
        }
        _gridController.ChangeTurn(1);
        yield return 0;
    }

}
