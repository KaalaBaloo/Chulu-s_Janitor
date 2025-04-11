using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueText;

public class DialogueManager
{
    [SerializeField] Image[,] _dialogueImages;

    public string[] GetDialogueTexts(int dialogue)
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                return null; //English.GetDialogues(dialogue);
            case 1: // Spanish
                Spanish spanish = new Spanish();
                string[] scene = GetTexts(spanish.GetDialogues(0));
                return scene;
            default:
                return null;
        }
    }

    private string[] GetTexts(List<Dialogue> dialogues)
    {
        string[] texts = new string[dialogues.Count];

        foreach (Dialogue dialogue in dialogues)
        {
            texts[dialogues.IndexOf(dialogue)] = dialogue.Text;
        }

        return texts;
    }
}
