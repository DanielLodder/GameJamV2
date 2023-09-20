using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //player movements
    [SerializeField] Rigidbody playerRB;
    [SerializeField] float movementSpeed;
    private PlayerInput playerInput;

    //player camera
    public GameObject camHolder;
    public GameObject playerObject;

    public float camSensitivity;
    public float rotationX = 0;
    public float rotationY = 0;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Look.performed += Rotation_performed;
        playerInputActions.Player.Move.performed += Move_performed;

    }
    public void Move_performed(InputAction.CallbackContext context)
    {
        Vector3 rawMove = context.ReadValue<Vector3>() * (movementSpeed * 10);

        Vector3 movementVector = transform.right * rawMove.x + transform.forward * rawMove.z;
        movementVector.y = playerRB.velocity.y;
        playerRB.velocity = movementVector;
    }
    public void Rotation_performed(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector2 lookVector = (camSensitivity * 10) * Time.fixedDeltaTime * context.ReadValue<Vector2>();

        rotationY += lookVector.x;
        rotationX -= lookVector.y;
        rotationX = Mathf.Clamp(rotationX, -85, 85);

        playerObject.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        camHolder.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
