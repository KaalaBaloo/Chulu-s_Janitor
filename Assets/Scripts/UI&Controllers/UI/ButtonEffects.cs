using UnityEngine;
using UnityEngine.UI;

public class ButtonEffects : MonoBehaviour
{
    [SerializeField] private AudioClip selectionSound;
    [SerializeField] private Color selectedColor = Color.magenta;
    [SerializeField] private float scaleMultiplier = 1.2f;

    private Text buttonText;
    private Vector3 originalScale;
    private AudioSource audioSource;

    private void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        originalScale = transform.localScale;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnSelect()
    {
        if (buttonText != null)
        {
            buttonText.color = selectedColor;
        }

        transform.localScale = originalScale * scaleMultiplier;

        if (audioSource != null && selectionSound != null)
        {
            audioSource.PlayOneShot(selectionSound);
        }
    }

    public void OnDeselect()
    {
        if (buttonText != null)
        {
            buttonText.color = Color.white;
        }

        transform.localScale = originalScale;
    }
}
