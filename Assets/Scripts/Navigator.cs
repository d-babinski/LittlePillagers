using System;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    private enum State
    {
        Reached = 0,
        Moving = 1
    }

    private const float SPEED_SCALING = 0.05f;
    
    public event Action OnPointReached = null;
    public Vector3 NextPosition => nextPos;
    
    private State currentState = State.Reached;
    private Vector3 target = Vector3.zero;
    private Vector3 nextPos = Vector3.zero;
    private float speed = 0f;
    
    public void MoveTo(Vector3 _target, float _speed)
    {
        currentState = State.Moving;
        target = _target;
        speed = _speed;
        nextPos = calculateNextPos();
    }

    private void Update()
    {
        if (currentState == State.Moving)
        {
            if (Vector3.Distance(transform.position, target) < Mathf.Epsilon)
            {
                currentState = State.Reached;
                OnPointReached?.Invoke();
                return;
            }
            
            nextPos = calculateNextPos();
        }
    }

    private Vector3 calculateNextPos()
    {
        return Vector3.MoveTowards(transform.position, target, speed * SPEED_SCALING * Time.deltaTime);
    }
}
