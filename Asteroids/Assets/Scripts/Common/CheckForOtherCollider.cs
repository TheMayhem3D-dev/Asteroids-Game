using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForOtherCollider
{
    public bool CircleCheck(Vector2 pos, float radius, LayerMask layerMask)
    {
        if (!Physics2D.OverlapCircle(pos, radius, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
