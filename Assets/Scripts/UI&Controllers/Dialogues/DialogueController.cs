using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] int _dialogueScene;

    private TMP_Text _text;
    private Image _dialogueImage;
    private GameObject _dialogueTotal;
    private GameObject _UI;

    private int _dialogueIndex = 0;
    private string[] _dialogues;
    private Sprite[] _sprites;

    void Start()
    {
        FindElements();

        if (_dialogues.Length != 0)
        {
            _UI.SetActive(false);
            _dialogues = DialogueManager.Instance.GetDialogueTexts(_dialogueScene);
            _sprites = DialogueManager.Instance.GetDialogueSprites(_dialogueScene);
            StartDialogue();
        }
    }

    private void FindElements()
    {
        _text = GameObject.FindGameObjectWithTag("_dialogueText").GetComponent<TMP_Text>();
        _dialogueImage = GameObject.FindGameObjectWithTag("_dialogueImage").GetComponent<Image>();
        _dialogueTotal = GameObject.FindGameObjectWithTag("_dialogue");
        _UI = GameObject.FindGameObjectWithTag("_canvasLevel");
    }

    private void StartDialogue()
    {
        _dialogueIndex = 0;
        _text.text = _dialogues[_dialogueIndex];
        _dialogueImage.sprite = _sprites[_dialogueIndex];
        _dialogueIndex++;
    }

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
            _dialogueImage.sprite = _sprites[_dialogueIndex];
            _dialogueIndex++;
        }
        else
        {
            _UI.SetActive(true);
            _dialogueTotal.SetActive(false);
        }
    }

}
