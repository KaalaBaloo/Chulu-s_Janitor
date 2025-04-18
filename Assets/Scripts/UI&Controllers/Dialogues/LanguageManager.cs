using System.Collections.Generic;
using UnityEngine;
using static LanguageData;

public class LanguageManager
{
    public static LanguageManager Instance { get; private set; }

    public string[] GetMainMenuTexts()
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                English english = new English();
                return english.uiTexts[0];
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return spanish.uiTexts[0];
            default:
                return null;
        }
    }

    public string[] GetPauseMenuTexts()
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                English english = new English();
                return english.uiTexts[0];
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return spanish.uiTexts[0];
            default:
                return null;
        }
    }

    public string[] GetExitTexts()
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                English english = new English();
                return english.uiTexts[1];
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return spanish.uiTexts[1];
            default:
                return null;
        }
    }

    public string[] GetPauseTexts()
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                English english = new English();
                return english.uiTexts[2];
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return spanish.uiTexts[2];
            default:
                return null;
        }
    }

    public string[] GetSettingsTexts()
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                English english = new English();
                return english.uiTexts[3];
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return spanish.uiTexts[3];
            default:
                return null;
        }
    }

    public string[] GetLevelNames()
    {
        switch (GeneralSettings.LANGUAGE)
        {
            case 0: // English
                English english = new English();
                return english.levelNames;
            case 1: // Spanish
                Spanish spanish = new Spanish();
                return spanish.levelNames;
            default:
                return null;
        }
    }
}
