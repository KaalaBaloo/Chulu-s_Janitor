using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SpawnOfC : Enemy
{
    private void Update()
    {

        MovePathFinding();

    }

    protected override void Attack()
    {
        _character.SubstractLife(_damage);
        StartCoroutine(AttackCoroutine(_rb, new Vector2(Mathf.RoundToInt(_character.transform.position.x) * _characterMovements,
            Mathf.RoundToInt(_character.transform.position.y) * _characterMovements)));
    }
}
