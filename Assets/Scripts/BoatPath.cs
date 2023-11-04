using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatPath : MonoBehaviour
{
    public List<Transform> points = null;
    
    private void Awake()
    {
        points = GetComponentsInChildren<Transform>().ToList();
        points.Remove(transform);
    }

}
