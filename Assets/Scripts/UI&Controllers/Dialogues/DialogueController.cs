using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    static bool _dialoguePlayed = false;

    [SerializeField] int _dialogueScene;

    private TMP_Text _text;
    private Image _dialogueImage;
    private GameObject _dialogueTotal;
    private GameObject _UI;

    private int _dialogueIndex = 0;
    private string[] _dialogues;
    private Sprite[] _sprites;

    private DialogueManager _dialogueManager;

    private void Awake()
    {
        FindElements();

        _dialogueManager = new DialogueManager();
        _dialogues = _dialogueManager.GetDialogueTexts(_dialogueScene);
        _sprites = _dialogueManager.GetDialogueSprites(_dialogueScene);
    }

    void Start()
    {
        if (!_dialoguePlayed)
            StartDialogue();
        else
            _dialogueTotal.SetActive(false);
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
        _UI.SetActive(false);
        _dialoguePlayed = true;
        _dialogueIndex = 0;
        _text.text = _dialogues[_dialogueIndex];
        _dialogueImage.sprite = _sprites[_dialogueIndex];
        _dialogueIndex++;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {
            ChangeDialogue();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
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
            Close();
        }
    }

    public void Close()
    {
        _dialogueTotal.SetActive(false);
        _UI.SetActive(true);
        Cursor.visible = false;
    }

}
