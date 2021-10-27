using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private void Awake()
    {
        if(Rb2d == null)
            Rb2d = GetComponent<Rigidbody2D>();
    }

    public void ApplyMovement(float input)
    {
        Vector2 movementVector = transform.up * input * movementSpeed;
        Rb2d.AddForce(movementVector, ForceMode2D.Force);
    }

    public void ApplyRotation(float input)
    {
        RotationAngle -= input * rotationSpeed;
        Rb2d.MoveRotation(Mathf.LerpAngle(Rb2d.rotation, RotationAngle, rotationSpeed * Time.deltaTime));
    }
}
