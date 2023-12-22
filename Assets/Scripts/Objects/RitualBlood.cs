using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualBlood : Sprites
{


    private void Start()
    {
        _spriteNumber = 0;
        _gridController.SetInteractive(6, _tileNumX, _tileNumY);
        _gridController.AddDirt();
    }
}
