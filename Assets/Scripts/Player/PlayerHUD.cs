using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactableNameText;
    [SerializeField] private Image crosshairImage;

    [Header("Crosshair Colors")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color interactableFoundColor;
    
    public void OnInteractableFound(IInteractable interactable)
    {
        interactableNameText.text = interactable.Name;
        crosshairImage.color = interactableFoundColor;
    }

    public void OnInteractableLost()
    {
        interactableNameText.text = string.Empty;
        crosshairImage.color = defaultColor;
    }
}
