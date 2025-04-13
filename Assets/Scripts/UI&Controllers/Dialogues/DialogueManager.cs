using System.Collections.Generic;
using UnityEngine;
using static LanguageData;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private Sprite[,] _dialogueImages; // assign this in Awake or load manually

    private Dictionary<string, Sprite> _dialogueImageDict;

    private void Awake()
    {
        LoadDictionary();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LoadDictionary()
    {
        _dialogueImageDict = new Dictionary<string, Sprite>();
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("Sprites/HUD&UI/Conversaciones");

        foreach (Sprite sprite in loadedSprites)
        {
            _dialogueImageDict[sprite.name] = sprite;
        }

        if (loadedSprites == null || loadedSprites.Length == 0)
        {
            return;
        }
    }

    public string[] GetDialogueTexts(int dialogue)
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                return null; // new English().GetDialogues(dialogue) etc.
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return GetTexts(spanish.GetDialogues(dialogue));
            default:
                return null;
        }
    }

    private string[] GetTexts(List<Dialogue> dialogues)
    {
        string[] texts = new string[dialogues.Count];
        for (int i = 0; i < dialogues.Count; i++)
        {
            texts[i] = dialogues[i].Text;
        }
        return texts;
    }

    public Sprite[] GetDialogueSprites(int dialogue)
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                return null; // new English().GetDialogues(dialogue) etc.
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return GetSprites(spanish.GetDialogues(dialogue));
            default:
                return null;
        }
    }

    private Sprite[] GetSprites(List<Dialogue> dialogues)
    {
        Sprite[] sprites = new Sprite[dialogues.Count];

        for (int i = 0; i < dialogues.Count; i++)
        {
            string spriteKey = dialogues[i].SpriteKey;

            if (_dialogueImageDict.TryGetValue(spriteKey, out Sprite sprite))
            {
                sprites[i] = sprite;
            }
            else
            {
                Debug.LogWarning($"Sprite not found for key: {spriteKey}");
            }
        }

        return sprites;
    }

    public Sprite GetSprite(string key)
    {
        if (_dialogueImageDict.TryGetValue(key, out Sprite sprite))
            return sprite;

        Debug.LogWarning($"Sprite not found for key: {key}");

        if (_dialogueImageDict.TryGetValue("MissingSprite", out Sprite fallback))
            return fallback;

        Debug.LogError("MissingSprite fallback not found in dictionary.");
        return null;
    }

}
