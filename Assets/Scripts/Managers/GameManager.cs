using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private PlayerHUD playerHUD;

    private void Awake()
    {
        playerInteract.OnInteractableFound += playerHUD.OnInteractableFound;
        playerInteract.OnInteractableLost += playerHUD.OnInteractableLost;
    }

    private void OnDestroy()
    {
        playerInteract.OnInteractableFound -= playerHUD.OnInteractableFound;
        playerInteract.OnInteractableLost -= playerHUD.OnInteractableLost;
    }
}
