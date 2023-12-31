using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SpawnOfC : Enemy
{
    bool _seen = false;
    [SerializeField] GameObject _VFXSeen;

    private void Update()
    {
        MovePathFinding();
        if (!_seen && !_patrol && _gridController.GetTurn() == 0)
        {
            _seen = true;
            Instantiate(_VFXSeen, transform.position, Quaternion.identity);
        }
        else if (_seen && _patrol && _gridController.GetTurn() == 0)
        {
            _seen = false;
        }
    }

    protected override void Attack()
    {
        _character.SubstractLife(_damage);
        StartCoroutine(AttackCoroutine(_rb, new Vector2(Mathf.RoundToInt(_character.transform.position.x) * _characterMovements,
            Mathf.RoundToInt(_character.transform.position.y) * _characterMovements)));
    }

}
