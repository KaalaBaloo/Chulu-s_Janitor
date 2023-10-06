using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Sprites : MonoBehaviour
{
    [SerializeField] protected int _spriteNumber;
    [SerializeField] protected int _tileNumX;
    [SerializeField] protected int _tileNumY;
    [SerializeField] protected float _speed = 0.25f;

    protected GameObject _gameController;
    protected GridController _gridController;

    //GRID BASE
    // 0 --> Libre
    // 1 --> Personaje
    // 2 --> Bloqueo
    // 3 --> Enemigo

    protected virtual void Awake()
    {
        _gameController = GameObject.FindWithTag("GameController");
        _gridController = _gameController.GetComponent<GridController>();
    }

    protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 position)
    {
        Vector2 endingposition = new Vector2 (Mathf.RoundToInt(rb.position.x + position.x), Mathf.RoundToInt(rb.position.y + position.y));
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
        yield return 0;
    }
}
