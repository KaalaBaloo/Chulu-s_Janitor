using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] Image _dialogueImage;
    [SerializeField] GameObject _dialogueTotal;
    GameObject _UI;

    [SerializeField] Sprite[] _sprites;

    [SerializeField] string[] _dialogues;
    [SerializeField] int[] _dialogueSprite;
    int _dialogueIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _UI = GameObject.FindGameObjectWithTag("CanvasLevels");
        if(_dialogues.Length != 0)
        {
            _UI.SetActive(false);
            _dialogueIndex = 0;
            _text.text = _dialogues[_dialogueIndex];
            _dialogueImage.sprite = _sprites[_dialogueSprite[_dialogueIndex]];
        }
        else
        {
            _UI.SetActive(true);
            _dialogueTotal.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDialogue();
        }
    }

    void ChangeDialogue()
    {
        if (_dialogueIndex < _dialogues.Length)
        {
            _text.text = _dialogues[_dialogueIndex];
            _dialogueImage.sprite = _sprites[_dialogueSprite[_dialogueIndex]];
            _dialogueIndex++;
        }
        else
        {
            _UI.SetActive(true);
            _dialogueTotal.SetActive(false);
        }
    }

}
