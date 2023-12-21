using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreDirt : Sprites
{
    private void Start()
    {
        _spriteNumber = 0;
        _gridController.SetInteractive(4, _tileNumX, _tileNumY);
        _gridController.AddDirt();
    }

}