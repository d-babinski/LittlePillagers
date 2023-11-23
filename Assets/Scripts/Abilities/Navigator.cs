using System;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    private const float SPEED_SCALING = 0.05f;

    public Vector3 MoveTo(Vector3 _target, float _speed)
    {
        return Vector2.MoveTowards(transform.position, _target, _speed * SPEED_SCALING * Time.deltaTime);
    }
}
