using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Boss_1 : Enemy
{
    [SerializeField] int _random = 0;
    bool _endStart = false;
    float t = 0;

    [SerializeField] GameObject _VFXTP;
    [SerializeField] GameObject _VFXDying;
    [SerializeField] GameObject _VFXDead;
    [SerializeField] GameObject _spriteChulu;

    private void Update()
    {
        if (_gridController.GetDirt() != 0)
        {
            MovePathFinding();
        }

        if (_gridController.GetEnd() && !_endStart)
        {
            _endStart = true;
            Instantiate(_VFXDying, transform.position, Quaternion.identity);
            StartCoroutine(EndCoroutine());
        }

    }

    protected override void Attack()
    {
        _character.SubstractLife(_damage);
        StartCoroutine(AttackCoroutine(_rb, new Vector2(Mathf.RoundToInt(_character.transform.position.x) * _characterMovements,
            Mathf.RoundToInt(_character.transform.position.y) * _characterMovements)));
    }

    override protected void MovePathFinding()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _moving = true;
            if (((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == 1
                        && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
            ||
                        (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == 1)
                        && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x)))
            {
                Attack();
            }
            else
            {
                _moving = true;
                _random = Random.Range(1, 5);
                if (_random == 1)
                {
                    StartCoroutine(TPCoroutine());
                }
                else
                {
                    if (_blockedView)
                    {
                        Debug.Log("PFBV");
                        PathFindingBlockView(_character.transform.position);
                    }
                    else
                    {
                        Debug.Log("PF");
                        PathFinding(_character.transform.position);
                    }
                }
            }
        }
    }

    virtual protected IEnumerator TPCoroutine()
    {
        bool tp = false;
        int x = Random.Range(1, 10);
        int y = Random.Range(1, 6);
        while (!tp)
        {
            x = Random.Range(1, 10);
            y = Random.Range(1, 6);
            if(_gridController.CanMove(x,y))
            {
                tp = true;
            }
            else 
            {
                yield return 0;
            }
        }
        _gridController.SetGrid(0, _tileNumX, _tileNumY);
        _tileNumX = Mathf.RoundToInt(x);
        _tileNumY = Mathf.RoundToInt(y);
        transform.position = new Vector3(x,y,0);
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        Instantiate(_VFXTP, transform.position, Quaternion.identity);
        SetChangeTurn();
        yield return 0;
    }

    virtual protected IEnumerator EndCoroutine()
    {
        while (t < 2)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Instantiate(_VFXDead, transform.position, Quaternion.identity);
        Instantiate(_spriteChulu, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        yield return 0;
    }


}
