using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LS_Buttons : MonoBehaviour
{
    [SerializeField] Sprite _on;
    [SerializeField] Sprite _off;
    SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = _off;
    }


    public void Available()
    {
        _sprite.sprite = _on;
    }
    
}
