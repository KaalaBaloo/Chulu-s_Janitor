using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeepOnes : Enemy
{
    [SerializeField] GameObject _marca;
    [SerializeField] bool _cargando = true;
    int _xMarca = 0;
    int _yMarca = 0;

    private void Update()
    {
        if (_cargando)
        {
            Debug.Log("Cargando");
            SelectPosition();
        }
        else
        {
            Debug.Log("!Cargando");
            Jump();
        }
    }

    void SelectPosition()
    {
        Vector2 posMarca;

        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _moving = true;
            posMarca = _customAIPathFinding.SearchPosition();
            if (posMarca.x > 0 && _gridController.GetGridTile(Mathf.RoundToInt(transform.position.x + posMarca.x), Mathf.RoundToInt(transform.position.y)) != 3 &&
                _gridController.GetGridTile(Mathf.RoundToInt(transform.position.x + posMarca.x), Mathf.RoundToInt(transform.position.y)) != 2)
            {
                _marca.transform.position = new Vector3 (transform.position.x + posMarca.x, transform.position.y, 0);
                _xMarca = Mathf.RoundToInt(posMarca.x);
                _cargando = false;
            }
            else if (posMarca.y > 0 && _gridController.GetGridTile(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + posMarca.y)) != 3 &&
                _gridController.GetGridTile(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y + posMarca.y)) != 2)
            {
                _marca.transform.position = new Vector3(transform.position.x, transform.position.y + posMarca.y, 0);
                _yMarca = Mathf.RoundToInt(posMarca.y);
                _cargando = false;
            }
            else if (CanJump(2,0))
            {
                _marca.transform.position = new Vector3(transform.position.x + 2, transform.position.y, 0);
                _xMarca = 2;
                _cargando = false;
            }
            else if (CanJump(-2, 0))
            {
                _marca.transform.position = new Vector3(transform.position.x - 2, transform.position.y, 0);
                _xMarca = -2;
                _cargando = false;
            }
            else if (CanJump(0, 2))
            {
                _marca.transform.position = new Vector3(transform.position.x, transform.position.y + 2, 0);
                _yMarca = 2;
                _cargando = false;
            }
            else if (CanJump(0, -2))
            {
                _marca.transform.position = new Vector3(transform.position.x, transform.position.y - 2, 0);
                _yMarca = -2;
                _cargando = false;
            }
            else
            {
                Debug.Log("Error, no es posible mover la marca");
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
        transform.position = endingposition;
        if (_gridController.GetGridTile(_tileNumX, _tileNumY) == 1)
        {
            _character.GameOver();
            Debug.Log("GameOver");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
