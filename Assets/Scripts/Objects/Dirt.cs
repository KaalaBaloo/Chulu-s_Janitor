using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Sprites
{

    private void Start()
    {
        _spriteNumber = 0;
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
        _gridController.SetDirtOnGrid(1, _tileNumX, _tileNumY);
        _gridController.AddDirt();
    }

}
