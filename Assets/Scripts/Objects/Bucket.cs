using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Sprites
{
    private void Start()
    {
        _spriteNumber = 0;
        _gridController.SetInteractive(5, _tileNumX, _tileNumY);
    }
}
