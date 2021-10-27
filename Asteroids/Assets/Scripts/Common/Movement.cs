using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] protected float rotationOffset = 90f;

    [Header("Stats")]
    [SerializeField] protected float maxMovementSpeed = 10f;
    [SerializeField] protected float movementSpeed = 10f;
    [SerializeField] protected float rotationSpeed = 4f;

    protected float rotationAngle = 0f;

    public Rigidbody2D Rb2d { get => rb2d; protected set => rb2d = value; }
    public float RotationAngle { get => rotationAngle; set => rotationAngle = value; }

    void FixedUpdate()
    {
        SpeedLimit();
    }

    private void SpeedLimit()
    {
        if (Rb2d.velocity.magnitude > maxMovementSpeed)
        {
            Rb2d.velocity = Rb2d.velocity.normalized * maxMovementSpeed;
        }
    }

    public virtual void RotateTowards(Vector2 direction)
    {
        if (direction.magnitude == 0) { return; }

        Vector2 lookDir = direction - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - rotationOffset;

        Rb2d.MoveRotation(Mathf.LerpAngle(Rb2d.rotation, angle, rotationSpeed * Time.deltaTime));
    }

    public virtual void RotateTowards(float angle)
    {
        Rb2d.rotation = angle;
    }

    public virtual void MoveTo(Vector2 direction)
    {
        Rb2d.MovePosition(Rb2d.position + direction * movementSpeed * Time.deltaTime);
    }
}
