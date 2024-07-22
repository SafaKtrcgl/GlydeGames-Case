using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Action<IInteractable> OnInteractableFound;
    public Action OnInteractableLost;

    [SerializeField] private Camera gameCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayDistance = 25f;

    private IInteractable _currentInteractable = null;

    private void FixedUpdate()
    {
        if (Physics.Raycast(gameCamera.transform.position, gameCamera.transform.forward, out var hitInfo, rayDistance, layerMask))
        {
            if (_currentInteractable is null && hitInfo.collider.TryGetComponent<IInteractable>(out _currentInteractable))
            {
                OnInteractableFound?.Invoke(_currentInteractable);
            }
        }
        else
        {
            if (_currentInteractable is not null)
            {
                _currentInteractable = null;
                OnInteractableLost?.Invoke();
            }
        }
    }

    public void Interact()
    {
        _currentInteractable?.OnPlayerInteract();
    }
}
