using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] MusicData musicData;
    [SerializeField] SFXData sfxData;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioClip GetCharacterSFX(string character, string type)
    {
        if(character == "Atal")
        {
            if (type == "move")
                return sfxData.Move[Random.Range(0, sfxData.Move.Length)];
            else if (type == "clean")
                return sfxData.Clean;
            else if (type == "wash")
                return sfxData.Wash;
            else if (type == "dirty")
                return sfxData.Dirty;
            else
                return null;
        }

        if(character == "Cultist")
        {
            if (type == "move")
                return sfxData.CultistMove;
            else if (type == "attack")
                return sfxData.CultistAttack;
            else
                return null;
        }

        if (character == "Amigo")
        {
            if (type == "move")
                return sfxData.AmigoMove;
            else if (type == "attack")
                return sfxData.AmigoAttack[Random.Range(0, sfxData.AmigoAttack.Length)];
            else
                return null;
        }

        if (character == "Deep")
        {
            if (type == "move")
                return sfxData.DeepMove;
            else if (type == "attack")
                return sfxData.DeepAttack;
            else
                return null;
        }

        if (character == "Mirror")
        {
            if (type == "move")
                return sfxData.MirrorMove;
            else if (type == "attack")
                return sfxData.MirrorAttack;
            else
                return null;
        }

        if (character == "Boss")
        {
            if (type == "move")
                return sfxData.BossMove;
            else if (type == "attack")
                return sfxData.BossAttack;
            else if (type == "teleport")
                return sfxData.BossTeleport;
            else if (type == "transform")
                return sfxData.BossTransform;
            else if (type == "rumble")
                return sfxData.BossRumble;
            else
                return null;
        }

        if (character == "Torns")
        {
            if (type == "in")
                return sfxData.TornsIn[Random.Range(0, sfxData.TornsIn.Length)];
            else if (type == "out")
                return sfxData.TornsOut[Random.Range(0, sfxData.TornsOut.Length)];
            else
                return null;
        }

        if(character == "Chulu")
        {
            if (type == "main")
                return sfxData.ChuluMain;
            else
                return null;
        }

        return null;
    }

    public AudioClip GetUIAudio(string type)
    {
        if (type == "mouse")
            return sfxData.Mouse;
        else if (type == "buttonClick")
            return sfxData.ButtonClick;
        else if (type == "buttonHover")
            return sfxData.ButtonHover;
        else if (type == "levelOpen")
            return sfxData.LevelOpen;
        else if (type == "levelClose")
            return sfxData.LevelClose;
        else if (type == "levelWin")
            return sfxData.Win;
        else if (type == "levelLose")
            return sfxData.Lose;
        else
            return null;
    }

    public AudioClip GetLevelMusic(string currentScene)
    {
        if (currentScene == "Main" || currentScene == "LevelSelector")
            return musicData.MenuMusic;
        else if (currentScene == "End")
            return musicData.CreditsMusic;
        else if (currentScene == "Comic01")
            return musicData.ComicsMusic[0];
        else if (currentScene == "Comic02")
            return musicData.ComicsMusic[1];
        else if (currentScene == "20_Battle")
            return musicData.BossMusic;
        else
            return musicData.LevelMusic[Random.Range(0, musicData.LevelMusic.Length)];
    }

    public AudioClip GetDialogueMusic(int character)
    {
        return musicData.DialoguesMusic[character];
    }
}
