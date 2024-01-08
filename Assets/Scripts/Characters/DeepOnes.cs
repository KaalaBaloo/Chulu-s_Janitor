using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class DeepOnes : Enemy
{
    [SerializeField] GameObject _marca;
    [SerializeField] GameObject _VFX;
    [SerializeField] GameObject _sprite;
    Animator _animator;
    [SerializeField] bool _cargando = true;
    int _xMarca = 0;
    int _yMarca = 0;
    AudioSource _audio;
    SpriteRenderer _srMarca;

    protected override void Start()
    {
        _spriteNumber = 3;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        _gridController.CreateEnemy();
        _characterLastTurn = _character.transform.position;
        _audio = GetComponent<AudioSource>();
        _animator = _sprite.GetComponent<Animator>();
        _srMarca = _marca.GetComponent<SpriteRenderer>();
        _srMarca.enabled = false;
    }

    private void Update()
    {
        if (!GridController.GAMEOVER)
        {
            if (_cargando)
            {
                SelectPosition();
            }
            else
            {
                Jump();
            }
        }
    }

    void SelectPosition()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _moving = true;

            if (Mathf.Abs(_character.transform.position.x - transform.position.x) >= Mathf.Abs(_character.transform.position.y - transform.position.y))
            {
                if (_character.transform.position.x > transform.position.x && GetCanMove(_tileNumX + 2, _tileNumY))
                {
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.x < transform.position.x && GetCanMove(_tileNumX - 2, _tileNumY))
                {
                    _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = -2;
                    _cargando = false;
                }
                else if (_character.transform.position.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = -2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX + 2, _tileNumY))
                {
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX - 2, _tileNumY))
                {
                    _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = -2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX, _tileNumY - 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = -2;
                    _cargando = false;
                }
                else
                {
                    Debug.Log("Error, no es posible mover la marca");
                }
            }
            else
            {
                if (_character.transform.position.y > transform.position.y && GetCanMove(_tileNumX, _tileNumY + 2))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.y < transform.position.y && GetCanMove(_tileNumX, _tileNumY - 2))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = -2;
                    _cargando = false;
                }
                else if (_character.transform.position.x > transform.position.x && GetCanMove(_tileNumX + 2, _tileNumY))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.x < transform.position.x && GetCanMove(_tileNumX - 2, _tileNumY))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = -2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 2))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX, _tileNumY + 2))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = -2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX + 2, _tileNumY))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (GetCanMove(_tileNumX - 2, _tileNumY))
                {
                    _srMarca.enabled = true;
                    _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = -2;
                    _cargando = false;
                }
                else
                {
                    Debug.Log("Error, no es posible mover la marca");
                }
            } 
            SetChangeTurn();
        }
    }

    void Jump()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            if (_gridController.GetGridTile(Mathf.RoundToInt(_marca.transform.position.x), Mathf.RoundToInt(_marca.transform.position.y)) != 3)
            {
                _moving = true;
                _gridController.SetGrid(0, _tileNumX, _tileNumY);
                _audio.Play();
                _animator.SetTrigger("jump");
                StartCoroutine(PositionCoroutine(_rb, _marca.transform.position));
                _tileNumX += _xMarca;
                _tileNumY += _yMarca;
            }
            else
            {
                Debug.Log("Error, no es posible moverse");
                _marca.transform.position = transform.position;
                RestartMarca();
                SetChangeTurn();
            }
        }
    }

    override protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 endingposition)
    {
        _srMarca.enabled = false;
        _marca.transform.position = transform.position;
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
        Instantiate(_VFX, transform.position, Quaternion.identity);
        transform.position = endingposition;
        if (_gridController.GetGridTile(_tileNumX, _tileNumY) == 1 && !GridController.GAMEOVER)
        {
            GridController.GAMEOVER = true;
            _character.GameOver();
            _gridController.GameOver();
        }
        else
        {
            _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
            RestartMarca();
            SetChangeTurn();
        }
        yield return 0;
    }

    void RestartMarca()
    {
        _srMarca.enabled = false;
        _cargando = true;
        _yMarca = 0;
        _xMarca = 0;
    }

}
