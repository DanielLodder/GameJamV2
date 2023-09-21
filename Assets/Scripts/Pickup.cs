using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] GameObject itemHolder;
    [SerializeField] Camera mainCamera;

    [SerializeField] private float pickupRange;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Fire.performed += ItemPickup;

    }
    public void ItemPickup(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            Debug.Log(hit);
            if (hit.collider.CompareTag("GrabableItem"))
            {
                hit.collider.transform.SetParent(itemHolder.transform);
            }
        }
    }
}
