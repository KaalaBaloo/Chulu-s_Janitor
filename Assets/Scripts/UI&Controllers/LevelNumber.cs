using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour
{
    TMP_Text _textLevelNumber;

    void Start()
    {
        _textLevelNumber = GetComponent<TMP_Text>();
        if (SceneManager.GetActiveScene().name != "20_Battle")
        {
            _textLevelNumber.text = (SceneManager.GetActiveScene().name).ToString();
        }
        else
        {
            _textLevelNumber.text = "20";
        }
    }
}
