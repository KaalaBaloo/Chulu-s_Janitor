using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "Audio/Music Data")]
public class MusicData : ScriptableObject
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip creditsMusic;
    [SerializeField] private AudioClip[] comicsMusic;
    [SerializeField] private AudioClip[] levelMusic;
    [SerializeField] private AudioClip[] dialoguesMusic;
    [SerializeField] private AudioClip bossMusic;

    // Optionally add getters if you need access from other scripts
    public AudioClip MenuMusic => menuMusic;
    public AudioClip CreditsMusic => creditsMusic;
    public AudioClip[] ComicsMusic => comicsMusic;
    public AudioClip[] LevelMusic => levelMusic;
    public AudioClip[] DialoguesMusic => dialoguesMusic;
    public AudioClip BossMusic => bossMusic;
}
