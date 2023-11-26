using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorOfC : Enemy
{
    [SerializeField] bool _cargando = false;
    [SerializeField] GameObject _Ray;

    private void Update()
    {
        MoveAttack();
    }

    void MoveAttack()
    {
        if (_gridController.GetTurn() == 0 && _moving)
        {
            _moving = false;
        }
        if (_gridController.GetTurn() == 1 && _character != null && !_moving && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            if ((_gridController.GetPlayerVerticalNoObstacle(_tileNumX,_tileNumY) || _gridController.GetPlayerHorizontalNoObstacle(_tileNumX,_tileNumY)))
            {
                _character.GameOver();
                _gridController.Restart();
            }
            if (!_cargando)
            {
                _moving = true;
                _customAIPathFinding.SearchPath();
                _cargando = true;
            }
            else if (_cargando)
            {
                _cargando = false;
                SetChangeTurn();
            }
        }
    }
}
