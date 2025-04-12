using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueText
{
    protected string[] uiTexts;
    protected string[] levelNames;
    protected string[] credits;

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

    protected List<List<Dialogue>> dialogues = new List<List<Dialogue>>();
    public virtual List<Dialogue> GetDialogues(int dialogue)
    {
        return this.dialogues[dialogue];
    }

}
