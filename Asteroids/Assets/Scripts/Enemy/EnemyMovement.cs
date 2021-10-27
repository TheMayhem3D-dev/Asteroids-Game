using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public override void MoveTo(Vector2 direction)
    {
        Rb2d.position = Vector2.MoveTowards(Rb2d.position, direction, movementSpeed * Time.deltaTime);
    }

    public override void RotateTowards(Vector2 direction)
    {
        Vector2 lookDir = direction - Rb2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - rotationOffset;

        Rb2d.rotation = angle;
    }
}
