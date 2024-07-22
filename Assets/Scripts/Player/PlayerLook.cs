using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;
    [SerializeField] private CharacterController playerController;

    [SerializeField] private const float xSensitivity = 10f;
    [SerializeField] private const float ySensitivity = 10f;


    private const float yUpperLimit = 60f;
    private const float yLowerLimit = -60f;

    private float xRotation = 0f;
    

    public void ProcessLookFromInput(Vector2 input)
    {
        xRotation -= (input.y * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, yLowerLimit, yUpperLimit);

        transform.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerController.transform.Rotate(Vector3.up * (input.x * Time.deltaTime * xSensitivity));
    }
}
