using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public CharacterController characterController;
    public FixedJoystick joystick;

    [Header("Settings")]
    public float speed = 3.0f;

    // Private Variables
    Vector3 input;
    Matrix4x4 matrix;

    void Start()
    {
        matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    }

    void Update()
    {
        GetInput();
        Move();
        FaceMovementDirection();
    }

    private void GetInput()
    {
        // calculate direction based upon the joystick
        input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
    }

    private void Move()
    {
        if (input.magnitude > 0.1)
            characterController.Move(speed * transform.forward * Time.deltaTime);
    }

    private void FaceMovementDirection()
    {
        if (input == Vector3.zero) return;

        // handle isometric direction
        Vector3 isomtericDirection = matrix.MultiplyPoint3x4 (input);

        transform.forward = isomtericDirection;
    }
}
