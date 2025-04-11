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
        public int[] Sprite;

        public Dialogue(string text, int[] sprite)
        {
            Text = text;
            Sprite = sprite;
        }
    }

    protected List<List<Dialogue>> dialogues = new List<List<Dialogue>>();
    public virtual List<Dialogue> GetDialogues(int dialogue)
    {
        return this.dialogues[dialogue];
    }

}
