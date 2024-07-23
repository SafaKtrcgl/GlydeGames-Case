using Singleton;
using UnityEngine;

public class InputHelper : MonoSingleton<InputHelper>
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerLook playerLook;
    [SerializeField] PlayerInteract playerInteract;

    private PlayerInput _playerInput;
    private PlayerInput.WalkingActions _walkingActions;

    public override void Awake()
    {
        base.Awake();
        _playerInput = new PlayerInput();
        _walkingActions = _playerInput.Walking;
        _walkingActions.Interact.performed += ctx => playerInteract.Interact();
    }

    public void OnDialogDisplay()
    {
        _playerInput.Walking.Disable();
        _playerInput.UI.Enable();
    }

    public void OnDialogHide()
    {
        _playerInput.Walking.Enable();
        _playerInput.UI.Disable();
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
