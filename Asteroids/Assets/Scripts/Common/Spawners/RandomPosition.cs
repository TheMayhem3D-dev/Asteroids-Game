using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomPosition
{
    [Header("PointsData")]
    public PointsData points;
    [HideInInspector] public int indexer;

    public Vector2 GetRandomPosition()
    {
        indexer = Random.Range(0, points.vectors.Capacity);
        Vector2 vector = GetRandomVector();
        return vector;
    }

    public Vector2 GetRandomPosition(int value)
    {
        indexer = value;
        Vector2 vector = GetRandomVector();
        return vector;
    }

    private Vector2 GetRandomVector()
    {
        float x = Random.Range(points.vectors[indexer].startPoint.x, points.vectors[indexer].endPoint.x);
        float y = Random.Range(points.vectors[indexer].startPoint.y, points.vectors[indexer].endPoint.y);
        Vector2 vector = new Vector2(x, y);
        return vector;
    }
}
