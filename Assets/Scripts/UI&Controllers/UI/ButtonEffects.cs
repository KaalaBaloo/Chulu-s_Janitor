using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private AudioClip selectionSound;
    [SerializeField] private Color selectedColor = Color.magenta;
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private Camera cameraToUse;

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

    public void OnPointerEnter(PointerEventData eventData) // Cambiar esto a envés de OnPointerEnter, que el audio
    {
        if (buttonText != null)
            buttonText.color = selectedColor;


        transform.localScale = originalScale * scaleMultiplier;

        //if (audioSource != null && selectionSound != null)
        //    audioSource.PlayOneShot(selectionSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = Color.white;

        transform.localScale = originalScale;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePosition = new Vector3(eventData.position.x, eventData.position.y, 0);

        var linkTaggedText = TMP_TextUtilities.FindIntersectingLink(buttonText, mousePosition, cameraToUse);

        if (linkTaggedText != -1)

            audioSource.PlayOneShot(selectionSound);
    }
}
