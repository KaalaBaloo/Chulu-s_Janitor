using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class English : LanguageData
{
    public English()
    {
        this.uiTexts = new List<string[]>
        {
            new string[]
            {
                "Play",
                "Settings",
                "Comic",
                "Credits",
                "Exit",
            },
            new string[]
            {
                "Wanna exit?",
                "Yes",
                "No",
            },
            new string[]
            {
                "Settings",
                "Fullscreen",
                "Silence",
                "Music",
                "Sound Effects",
                "Low Resolution",
                "Medium Resolution",
                "High Resolution",
                "Ultra Resolution",
                "English",
                "Spanish",
                "Back",
            }
        };

        this.levelNames = new string[]
        {
            "1 - CAPITALISM, HERE I COME!",
            "2 - OUCH! MY LITTLE FEET…",
            "3 - WHAT THE HECK IS THAT?",
            "4 - DON’T POINT THOSE SHARP THINGS AT ME",
            "5 - WAIT, NOW THERE’S TWO?",
            "6 - GET OFF ME, BUG!",
            "7 - YOU AGAIN?",
            "8 - OH, THAT GETS DIRTY TOO?",
            "9 - THERE GO MY FISH STICKS",
            "10 - WELL, THIS IS A FINE MESS",
            "11 - SOOO... WHEN DO I GET PAID?",
            "12 - SCRUBBING SIDE TO SIDE",
            "13 - HELP, I NEED PERSONAL SPACE",
            "14 - FEELING LIKE BOND TODAY",
            "15 - ARE YOU... A FOLLICLE?",
            "16 - YEAH, THAT’S DEFINITELY NOT A FOLLICLE",
            "17 - I DON’T GET PAID ENOUGH FOR THIS",
            "18 - BUY ONE, GET ONE CHAOS",
            "19 - ME WORKING WHILE EVERYONE ELSE PARTIES",
            "20 - . . ."
        };

        this.credits = new string[]
        {
            "Desarrollador: Juan Pérez",
            "Artista: Ana Gómez",
            "Música: Carlos López",
            "Agradecimientos especiales a todos los que apoyaron este proyecto."
        };

        this.dialogues = new List<List<Dialogue>>
        {
            new List<Dialogue> // Scene 1 - Nyar - Introduction
            {
                new Dialogue("Hey there! You've been chosen for a task of cosmic proportions.", "Nyar_02"),
                new Dialogue("But is this really the one? Kinda has a silly face...", "Nyar_01"),
                new Dialogue("No no, if I get picky I’ll run out of time.", "Nyar_03"),
                new Dialogue("Come on, wake up, time flies!", "Nyar_02")
            },
            new List<Dialogue> // Scene 2 - Cultist - Level 1
            {
                new Dialogue("An applicant! I didn't think anyone would take this seriously…", "Cultist_01"),
                new Dialogue("Let alone actually show up...", "Cultist_03"),
                new Dialogue("Your job is simple, but since the last one fell into a trap, another got eaten alive, and the one before that disintegrated on the spot, I’ll have to explain it.", "Cultist_03"),
                new Dialogue("The place is obviously a mess, rituals are messy business. So it's up to you to clean it up.", "Cultist_02"),
                new Dialogue("Best of luck... If you die, at least do it in an easy-to-clean spot.", "Cultist_01")
            },
            new List<Dialogue> // Scene 3 - Level 3
            {
                new Dialogue("Well well, you survived your first day. One more and you break the record.", "Cultist_02"),
                new Dialogue("By the way, I forgot to mention that the Ministry of Workers' Rights requires us to inform you that your health insurance does not cover: falls, slips, burns, traps, monster attacks, or death, among others.", "Cultist_01"),
                new Dialogue("Keep up the good work!", "Cultist_01")
            },
            new List<Dialogue> // Scene 4 - Level 6
            {
                new Dialogue("Nice job, I didn’t think much of you but you’re doing pretty well.", "Cultist_01"),
                new Dialogue("Needless to say, the deeper you go into the dungeon the more ritual aftermaths you'll see.", "Cultist_02"),
                new Dialogue("Some are friendly, others not so much...", "Cultist_02"),
                new Dialogue("You know the drill, so keep it up.", "Cultist_01")
            },
            new List<Dialogue> // Scene 5 - Level 8
            {
                new Dialogue("Remember it's not a magic mop, it needs a rinse every now and then.", "Cultist_02"),
                new Dialogue("Use the bucket, please. It’s dirtier than before now.", "Cultist_03"),
                new Dialogue("Also, we’re getting closer to awakening the great master, so the messes will only get worse.", "Cultist_01")
            },
            new List<Dialogue> // Scene 6 - Level 12
            {
                new Dialogue("Did you know the ancient texts say seeing our great lord drives you insane?", "Cultist_02"),
                new Dialogue("…", "Cultist_03"),
                new Dialogue("Hope that doesn’t happen to us, I’m actually excited to see our great lord.", "Cultist_01")
            },
            new List<Dialogue> // Scene 7 - Level 15
            {
                new Dialogue("Hey. Have you seen Follicle by any chance?", "Cultist_01"),
                new Dialogue("He said he was going to be part of the new ritual, carried a super flashy mirror and talked about a sacred light...", "Cultist_02"),
                new Dialogue("If you see him, let me know.", "Cultist_01")
            },
            new List<Dialogue> // Scene 8 - Level 19
            {
                new Dialogue("Finally, the big day is here. Aren’t you excited?", "Cultist_01"),
                new Dialogue("What? They didn’t tell you anything? That was Follicle’s job... Guess I’ll have to fill you in again.", "Cultist_03"),
                new Dialogue("Today’s the day we summon the great master.", "Cultist_02"),
                new Dialogue("When you're done with this room, head to the next one to receive him.", "Cultist_01")
            },
            new List<Dialogue> // Scene 9 - Level 20
            {
                new Dialogue("Hate to break it to you just as you arrive, but we’re heading out.", "Cultist_01"),
                new Dialogue("The ritual was a failure and we didn’t manage to bring back the great master.", "Cultist_02"),
                new Dialogue("I’m heading home. You can leave when you finish cleaning up.", "Cultist_03")
            },
            new List<Dialogue> // Scene 10 - Level 20
            {
                new Dialogue("At last, I am freed from my deep slumber...", "Cthulhu_01"),
                new Dialogue("As a reward for releasing me, your soul shall be the first course of this feast.", "Cthulhu_01"),
                new Dialogue("...", "Cthulhu_01"),
                new Dialogue("Are you even listening to me?", "Cthulhu_02")
            },
            new List<Dialogue> // Scene 11 - Ending
            {
                new Dialogue("A mere mortal like you cannot delay judgment day...", "Cthulhu_01"),
                new Dialogue("Still not listening...", "Cthulhu_02"),
                new Dialogue("No matter, your arrogance and foolishness are but a small bump in my illustrious plan...", "Cthulhu_01"),
                new Dialogue("I shall defeat the foolish sleeper and finally rule over the entire universe!", "Cthulhu_01"),
                new Dialogue("Bold words for someone losing to a mortal with a mop.", "Nyar_02"),
                new Dialogue("No! You’re not supposed to Be hEre…", "Cthulhu_01"),
                new Dialogue("Chu!", "Chulu_01"),
                new Dialogue("Who would’ve thought, you were the chosen one after all…", "Nyar_02"),
                new Dialogue("...", "Nyar_02"),
                new Dialogue("Well… if we’re being honest, it’s all thanks to me. After all, I was the one who picked you *and* landed the final blow… Yep, all me.", "Nyar_01"),
                new Dialogue("Now that you're here, could you do me one last favor?", "Nyar_02"),
                new Dialogue("I was only able to weaken his powers, so I’m still not sure what might happen.", "Nyar_03"),
                new Dialogue("I need you to take him home and look after him so he doesn’t cause any trouble, alright?", "Nyar_02"),
                new Dialogue("...", "Nyar_01"),
                new Dialogue("I didn’t hear a 'no' so that’s a 'yes' in my book!", "Nyar_02")
            }
        };

    }

    public override List<Dialogue> GetDialogues(int dialogue) => base.GetDialogues(dialogue);

}
