using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PointSet : MonoBehaviour
{
    [SerializeField] private Transform[] points = null;

    public Vector3 GetRandom()
    {
        if (points.Length <= 0)
        {
            return transform.position;
        }
        
        return points[Random.Range(0, points.Length)].position;
    }
}
