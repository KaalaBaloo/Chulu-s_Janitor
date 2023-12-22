using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitualPent : Sprites
{
    SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_gridController.GetRitual())
        {
            _sprite.color = Color.yellow;
        }
    }
}
