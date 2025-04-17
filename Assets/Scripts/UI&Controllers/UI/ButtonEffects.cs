using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip selectionSound;
    [SerializeField] private Color selectedColor = Color.magenta;
    [SerializeField] private float scaleMultiplier = 1.2f;

    private TextMeshProUGUI buttonText;
    private Vector3 originalScale;
    private AudioSource audioSource;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (audioSource != null && selectionSound != null)
        {
            audioSource.volume = GeneralSettings.SFXVOLUME / 100f;
        }
        if (GeneralSettings.MUTED)
        {
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = selectedColor;

        transform.localScale = originalScale * scaleMultiplier;

        if (audioSource != null && selectionSound != null)
            audioSource.PlayOneShot(selectionSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = Color.white;

        transform.localScale = originalScale;
    }
}
