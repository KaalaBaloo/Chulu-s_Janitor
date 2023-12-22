using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : Sprites
{
    private void Start()
    {
        _spriteNumber = 0;
        _gridController.SetInteractive(1, _tileNumX, _tileNumY);
        _gridController.AddDirt();
    }

}
