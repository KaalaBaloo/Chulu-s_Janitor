using System.Collections;
using UnityEngine;

public class Wall : Sprites
{
    private void Start()
    {
        _spriteNumber = 2;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
    }
}
