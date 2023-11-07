using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Sprites
{
    private void Start()
    {
        _spriteNumber = 1;
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        _gridController.AddDirt();
    }
}
