using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepOnes : Enemy
{
    [SerializeField] GameObject _marca;
    [SerializeField] bool _cargando = true;

    private void Update()
    {
        if (_cargando)
        {
            MovePathFinding();
        }
        else
        {
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
            if (posMarca.x > 0 && GetCanMove(Mathf.RoundToInt(posMarca.x), 0))
            {
                _marca.transform.position = new Vector3 (posMarca.x, 0, 0);
                _cargando = false;
            }
            else if (posMarca.y > 0 && GetCanMove(0, Mathf.RoundToInt(posMarca.y)))
            {
                _marca.transform.position = new Vector3( 0, posMarca.y, 0);
                _cargando = false;
            }
            else
            {
                Debug.Log("Error, no es posible moverse");
            }
            SetChangeTurn();
        }
    }

    void Jump()
    {
        PositionCoroutine(_rb, _marca.transform.position);
    }

    override protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 endingposition)
    {
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
}
