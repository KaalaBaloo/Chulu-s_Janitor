using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MirrorOfC : Enemy
{
    [SerializeField] bool _cargando = false;
    [SerializeField] GameObject _leftRay;
    [SerializeField] GameObject _rightRay;
    [SerializeField] GameObject _topRay;
    [SerializeField] GameObject _downRay;
    [SerializeField] LayerMask mascara;

    protected override void Start()
    {
        _spriteNumber = 3;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        _gridController.CreateEnemy();
        _characterLastTurn = _character.transform.position;
        RayCast();
    }

    private void Update()
    {
        if (!GridController.GAMEOVER)
        {
            Attack();
            MovePathFinding();
        }
    }

    override protected void MovePathFinding()
    {
        if (_gridController.GetTurn() == 1 && _character != null && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _gridController.ChangeTurn(2);
            if (!_cargando)
            {
                _cargando = true;
                PathFinding(_characterLastTurn);
            }
            else if (_cargando)
            {
                _cargando = false;
                SetChangeTurn();
            }
            _characterLastTurn = _character.transform.position;
        }
    }

    protected override void SetChangeTurn()
    {
        RayCast();
        _gridController.EnemyMoved();
    }

    protected override void Attack()
    {
        if ((_gridController.GetPlayerVerticalNoObstacle(_tileNumX, _tileNumY) || _gridController.GetPlayerHorizontalNoObstacle(_tileNumX, _tileNumY)) && !GridController.GAMEOVER)
        {
            GridController.GAMEOVER = true;
            _character.GameOver();
            _gridController.GameOver();
        }
    }

    void RayCast()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 12f,  mascara);     
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 12f, mascara);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 12f, mascara);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 12f, mascara);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 12f, Color.red, 0.5f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 12f, Color.red, 0.5f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 12f, Color.red, 0.5f);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 12f, Color.red, 0.5f);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * hitRight.distance, Color.blue, 1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * hitLeft.distance, Color.blue, 1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * hitUp.distance, Color.blue, 1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * hitDown.distance, Color.blue, 1);

        _leftRay.transform.position = new Vector2(transform.position.x - hitLeft.distance/2, transform.position.y);
        _topRay.transform.position = new Vector2(transform.position.x, transform.position.y + hitUp.distance/2);
        _rightRay.transform.position = new Vector2(transform.position.x + hitRight.distance/2, transform.position.y);
        _downRay.transform.position = new Vector2(transform.position.x, transform.position.y - hitDown.distance/2);
        _leftRay.GetComponent<SpriteRenderer>().size = new Vector2(hitLeft.distance * 11.5f, _leftRay.GetComponent<SpriteRenderer>().size.y);
        _topRay.GetComponent<SpriteRenderer>().size = new Vector2(_topRay.GetComponent<SpriteRenderer>().size.x, hitUp.distance * 11.5f);
        _rightRay.GetComponent<SpriteRenderer>().size = new Vector2(hitRight.distance * 11.5f, _rightRay.GetComponent<SpriteRenderer>().size.y);
        _downRay.GetComponent<SpriteRenderer>().size = new Vector2(_downRay.GetComponent<SpriteRenderer>().size.x, hitDown.distance * 11.5f);
    }
}
