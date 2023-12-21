using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Sprites
{
    private void Start()
    {
        _spriteNumber = 2;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
    }
}
