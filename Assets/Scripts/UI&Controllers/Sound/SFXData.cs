using UnityEngine;

[CreateAssetMenu(fileName = "SFXData", menuName = "Audio/SFX Data")]
public class SFXData : ScriptableObject
{
    [Header("Main Character Atal")]
    [SerializeField] private AudioClip[] move;
    [SerializeField] private AudioClip clean;
    [SerializeField] private AudioClip wash;
    [SerializeField] private AudioClip dirty;

    [Header("Game Events")]
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;

    [Header("Enemies")]
    [Header("Cultist")]
    [SerializeField] private AudioClip cultistMove;
    [SerializeField] private AudioClip cultistAttack;
    [SerializeField] private AudioClip cultistDetected;

    [Header("Amigo")]
    [SerializeField] private AudioClip amigoMove;
    [SerializeField] private AudioClip[] amigoAttack;

    [Header("Deep")]
    [SerializeField] private AudioClip deepMove;
    [SerializeField] private AudioClip deepAttack;

    [Header("Mirror")]
    [SerializeField] private AudioClip mirrorMove;
    [SerializeField] private AudioClip mirrorAttack;

    [Header("Boss")]
    [SerializeField] private AudioClip bossMove;
    [SerializeField] private AudioClip bossAttack;
    [SerializeField] private AudioClip bossTeleport;
    [SerializeField] private AudioClip bossTransform;
    [SerializeField] private AudioClip bossRumble;

    [Header("Chulu")]
    [SerializeField] private AudioClip chuluMain;

    [Header("Objects")]
    [SerializeField] private AudioClip[] spikesIn;
    [SerializeField] private AudioClip[] spikesOut;

    [Header("UI")]
    [SerializeField] private AudioClip mouse;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip buttonHover;
    [SerializeField] private AudioClip levelOpen;
    [SerializeField] private AudioClip levelClose;


    // Optional: Public getters (useful for code access without exposing Inspector)
    public AudioClip[] Move => move;
    public AudioClip Clean => clean;
    public AudioClip Wash => wash;
    public AudioClip Dirty => dirty;
    public AudioClip Win => win;
    public AudioClip Lose => lose;

    public AudioClip CultistMove => cultistMove;
    public AudioClip CultistAttack => cultistAttack;
    public AudioClip CultistDetected => cultistDetected;

    public AudioClip AmigoMove => amigoMove;
    public AudioClip[] AmigoAttack => amigoAttack;

    public AudioClip DeepMove => deepMove;
    public AudioClip DeepAttack => deepAttack;

    public AudioClip MirrorMove => mirrorMove;
    public AudioClip MirrorAttack => mirrorAttack;

    public AudioClip BossMove => bossMove;
    public AudioClip BossAttack => bossAttack;
    public AudioClip BossTeleport => bossTeleport;
    public AudioClip BossTransform => bossTransform;
    public AudioClip BossRumble => bossRumble;

    public AudioClip ChuluMain => chuluMain;

    public AudioClip[] SpikesIn => spikesIn;
    public AudioClip[] SpikesOut => spikesOut;

    public AudioClip Mouse => mouse;
    public AudioClip ButtonClick => buttonClick;
    public AudioClip ButtonHover => buttonHover;
    public AudioClip LevelOpen => levelOpen;
    public AudioClip LevelClose => levelClose;

}
