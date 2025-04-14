using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LanguageData
{
    public List<string[]> uiTexts;
    public string[] levelNames;
    public string[] credits;

    public struct Dialogue
    {
        public string Text;
        public string SpriteKey;

        public Dialogue(string text, string sprite)
        {
            this.Text = text;
            this.SpriteKey = sprite;
        }
    }

    protected List<List<Dialogue>> dialogues;
    public virtual List<Dialogue> GetDialogues(int dialogue)
    {
        return this.dialogues[dialogue];
    }

}
