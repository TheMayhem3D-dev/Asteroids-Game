using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Camera cam;

    private Vector2 mouseVector;
    private Vector2 keyboardVector;
    private Vector2 mousePosition;
    private Vector2 mouseMagnitude;

    [Header("Input")]
    private InputMasterActions inputMasterActions;
    private InputAction movementAction;
    private InputAction mousePositionAction;
    private InputAction mouseAxisAction;
    private bool rotationControlByMouse;


    private void Awake()
    {
        if(playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        if (cam == null)
            cam = Camera.main;

        inputMasterActions = new InputMasterActions();
    }

    private void OnEnable()
    {
        movementAction = inputMasterActions.Player.Movement;
        movementAction.Enable();

        mousePositionAction = inputMasterActions.Player.MousePosition;
        mousePositionAction.Enable();

        mouseAxisAction = inputMasterActions.Player.Mouse;
        mouseAxisAction.Enable();

        inputMasterActions.Player.Shoot.performed -= weapon.ShootAction;
        inputMasterActions.Player.Shoot.performed += weapon.ShootAction;
        inputMasterActions.Player.Shoot.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
        mousePositionAction.Disable();
        mouseAxisAction.Disable();
        inputMasterActions.Player.Shoot.Disable();
    }

    private void FixedUpdate()
    {
        SetVectors();

        if (mouseMagnitude.magnitude != 0 && keyboardVector.x == 0)
        {
            rotationControlByMouse = true;
        }
        else if (mouseMagnitude.magnitude == 0 && keyboardVector.x != 0)
        {
            rotationControlByMouse = false;
        }

        SetVectors();
        MouseInput();
        KeyboardInput();
    }

    private void SetVectors()
    {
        mouseVector = mousePositionAction.ReadValue<Vector2>();
        mouseMagnitude = mouseAxisAction.ReadValue<Vector2>();
        mousePosition = cam.ScreenToWorldPoint(mouseVector);
        keyboardVector = movementAction.ReadValue<Vector2>();
    }

    private void KeyboardInput()
    {
        if (!rotationControlByMouse)
        {
            playerMovement.ApplyRotation(keyboardVector.x);
        }

        if (keyboardVector.y != 0)
            playerMovement.ApplyMovement(keyboardVector.y);
    }

    private void MouseInput()
    {
        if (rotationControlByMouse)
            playerMovement.RotateTowards(mousePosition);
    }
}
