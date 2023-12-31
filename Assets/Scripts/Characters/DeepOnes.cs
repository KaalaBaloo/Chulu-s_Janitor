using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class DeepOnes : Enemy
{
    [SerializeField] GameObject _marca;
    [SerializeField] GameObject _VFX;
    [SerializeField] bool _cargando = true;
    int _xMarca = 0;
    int _yMarca = 0;

    private void Update()
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

    void SelectPosition()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _moving = true;

            if (Mathf.Abs(_character.transform.position.x - transform.position.x) > Mathf.Abs(_character.transform.position.y - transform.position.y))
            {
                if (_character.transform.position.x > transform.position.x && CanJump(2, 0))
                {
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.x < transform.position.x && CanJump(-2, 0))
                {
                    _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = -2;
                    _cargando = false;
                }
                else if (_character.transform.position.y > transform.position.y && CanJump(0, 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.y < transform.position.y && CanJump(0, -2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = -2;
                    _cargando = false;
                }
                else if (CanJump(2, 0))
                {
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (CanJump(-2, 0))
                {
                    _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = -2;
                    _cargando = false;
                }
                else if (CanJump(0, 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (CanJump(0, -2))
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
                if (_character.transform.position.y > transform.position.y && CanJump(0, 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.y < transform.position.y && CanJump(0, -2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = -2;
                    _cargando = false;
                }
                else if (_character.transform.position.x > transform.position.x && CanJump(2, 0))
                {
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (_character.transform.position.x < transform.position.x && CanJump(-2, 0))
                {
                    _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = -2;
                    _cargando = false;
                }
                else if (CanJump(0, 2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = 2;
                    _cargando = false;
                }
                else if (CanJump(0, -2))
                {
                    _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _yMarca = -2;
                    _cargando = false;
                }
                else if (CanJump(2, 0))
                {
                    _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                    _marca.GetComponent<Renderer>().enabled = true;
                    _xMarca = 2;
                    _cargando = false;
                }
                else if (CanJump(-2, 0))
                {
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
        _marca.GetComponent<Renderer>().enabled = false;
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
        if (_gridController.GetGridTile(_tileNumX, _tileNumY) == 1)
        {
            _character.GameOver();
            _gridController.Restart();
        }
        else
        {
            _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
            RestartMarca();
            SetChangeTurn();
        }
        yield return 0;
    }


    bool CanJump(int x, int y)
    {
        if (_gridController.GetGridTile(Mathf.RoundToInt(transform.position.x + x), Mathf.RoundToInt(transform.position.y + y)) != 3 &&
                _gridController.GetGridTile(Mathf.RoundToInt(transform.position.x + x), Mathf.RoundToInt(transform.position.y + y)) != 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void RestartMarca()
    {
        _cargando = true;
        _yMarca = 0;
        _xMarca = 0;
    }

}
