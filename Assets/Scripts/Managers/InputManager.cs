using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerLook playerLook;
    [SerializeField] PlayerInteract playerInteract;

    private PlayerInput _playerInput;
    private PlayerInput.WalkingActions _walkingActions;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _walkingActions = _playerInput.Walking;
        _walkingActions.Interact.performed += ctx => playerInteract.Interact();
    }

    private void FixedUpdate()
    {
        playerMovement.ProcessMovementFromInput(_walkingActions.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerLook.ProcessLookFromInput(_walkingActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
