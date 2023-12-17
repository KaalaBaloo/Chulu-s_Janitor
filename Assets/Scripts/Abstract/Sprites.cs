using System.Collections;
using TMPro;
using UnityEngine;

public abstract class Sprites : MonoBehaviour
{
    [SerializeField] protected int _spriteNumber;
    [SerializeField] protected int _tileNumX;
    [SerializeField] protected int _tileNumY;
    [SerializeField] protected float _speed = 0.25f;

    protected GameObject _gameController;
    protected GridController _gridController;
    protected int _characterMovements = 1;

    //GRID BASE
    // 0 --> Libre
    // 1 --> Personaje
    // 2 --> Bloqueo
    // 3 --> Enemigo

    protected virtual void Awake()
    {
        _gameController = GameObject.FindWithTag("GameController");
        _gridController = _gameController.GetComponent<GridController>();
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
    }

    virtual protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 position) { yield return 0; }
}
