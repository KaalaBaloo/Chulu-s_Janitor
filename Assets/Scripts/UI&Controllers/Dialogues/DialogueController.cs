using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    static List<bool> _dialoguePlayed = new List<bool>();

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
        if (_dialogueScene < _dialoguePlayed.Count)
        {
            if (_dialoguePlayed[_dialogueScene])
            {
                _dialogueTotal.SetActive(false);
                Close();
            }
            else
            {
                _dialoguePlayed[_dialogueScene] = true;
                StartDialogue();
            }
        }
        else
        {
            while (_dialoguePlayed.Count <= _dialogueScene)
            {
                _dialoguePlayed.Add(false);
            }

            _dialoguePlayed[_dialogueScene] = true;
            StartDialogue();
        }

        Debug.Log("Dialogue played: " + _dialoguePlayed.Count);
        Debug.Log("Dialogue index: " + _dialogueScene);
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
        _dialogueIndex = 0;
        _text.text = _dialogues[_dialogueIndex];
        _dialogueImage.sprite = _sprites[_dialogueIndex];
        _dialogueIndex++;
        Cursor.visible = true;
        MusicManager.Instance.PlayDialogueMusic();
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
        MusicManager.Instance.PlayLevelMusic();
    }

}
