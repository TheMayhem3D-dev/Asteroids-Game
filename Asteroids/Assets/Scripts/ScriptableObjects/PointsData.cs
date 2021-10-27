using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointsData", menuName = "Data/Points")]
public class PointsData : ScriptableObject
{
    public List<Vector2Data> vectors;
}
