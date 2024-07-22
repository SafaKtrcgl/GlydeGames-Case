using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] private float movementSpeed = 5f;

    public void ProcessMovementFromInput(Vector2 input)
    {
        characterController.Move((transform.forward * input.y + transform.right * input.x) * (movementSpeed * Time.deltaTime));
    }
}
