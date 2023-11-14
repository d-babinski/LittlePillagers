using System;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    private enum State
    {
        Reached = 0,
        Moving = 1
    }

    public bool HasReachedDestination => currentState == State.Reached;
    private const float SPEED_SCALING = 0.05f;
    
    public event Action OnPointReached = null;
    public float Speed = 1f;
    public Vector3 NextPosition => nextPos;
    
    private State currentState = State.Reached;
    private Vector3 target = Vector3.zero;
    private Vector3 nextPos = Vector3.zero;
    
    public void MoveTo(Vector3 _target)
    {
        currentState = State.Moving;
        target = _target;
        nextPos = calculateNextPos(Speed);
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
            
            nextPos = calculateNextPos(Speed);
        }
    }

    private Vector3 calculateNextPos(float _speed)
    {
        return Vector3.MoveTowards(transform.position, target, _speed * SPEED_SCALING * Time.deltaTime);
    }
}
