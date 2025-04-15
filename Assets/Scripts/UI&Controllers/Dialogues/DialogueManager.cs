using System.Collections.Generic;
using UnityEngine;
using static LanguageData;

public class DialogueManager
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private Sprite[,] _dialogueImages;

    private Dictionary<string, Sprite> _dialogueImageDict;

    private void LoadDictionary()
    {
        _dialogueImageDict = new Dictionary<string, Sprite>();
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>("Sprites/HUD&UI/Conversaciones");
        Debug.Log($"Loaded {loadedSprites.Length} sprites from Sprites/HUD&UI/Conversaciones");

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
                English english = new English();
                return GetTexts(english.GetDialogues(dialogue));
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
        LoadDictionary();
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                English english = new English();
                return GetSprites(english.GetDialogues(dialogue));
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return GetSprites(spanish.GetDialogues(dialogue));
            default:
                return null;
        }
    }

    private Sprite[] GetSprites(List<Dialogue> dialogues)
    {
        LoadDictionary();
        Sprite[] sprites = new Sprite[dialogues.Count];

        for (int i = 0; i < dialogues.Count; i++)
        {
            string spriteKey = dialogues[i].SpriteKey;

            if (_dialogueImageDict.TryGetValue(spriteKey, out Sprite sprite))
            {
                sprites[i] = sprite;
            }
        }

        return sprites;
    }

    public Sprite GetSprite(string key)
    {
        LoadDictionary();
        if (_dialogueImageDict.TryGetValue(key, out Sprite sprite))
            return sprite;

        return null;
    }

}
