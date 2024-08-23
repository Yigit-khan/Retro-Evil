using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public float lookSensitivity = 1f;

    [Header("References")]
    public CharacterController characterController;
    public Transform cameraTransform;

    private Vector3 velocity;
    private bool isGrounded;

    private void Update()
    {
        HandleMovement();
        HandleLook();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        isGrounded = characterController.isGrounded;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraTransform.Rotate(Vector3.left * mouseY);
        // Kamera açýsýný sýnýrla
        Vector3 cameraRotation = cameraTransform.localEulerAngles;
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -90f, 90f);
        cameraTransform.localEulerAngles = cameraRotation;
    }

    private void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}