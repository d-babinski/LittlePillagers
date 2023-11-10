using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Ship : MonoBehaviour
{
    private enum State
    {
        Waiting = 0,
        Moving = 1,
        Sunk = 2,
    }

    public UnitType UnitType = null;
    public StringVariable ShipName = null;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Animator Animator => animator;
    public bool HasSunk => currentState == State.Sunk;
    public bool IsMoving => currentState == State.Moving;

    private State currentState = State.Waiting;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Navigator navigator = null;

    public void MoveTo(Vector3 _pos)
    {
        navigator.MoveTo(_pos);
        currentState = State.Moving;
        animator.SetTrigger("Swim");
    }

    public void Sink()
    {
        animator.SetTrigger("Sink");
        currentState = State.Sunk;
    }

    public void Wait()
    {   
        animator.SetTrigger("Idle");
        currentState = State.Waiting;
    }

    private void Update()
    {
        if (currentState is State.Moving)
        {
            spriteRenderer.flipX = navigator.NextPosition.x > transform.position.x;
            transform.position = navigator.NextPosition;

            if (navigator.HasReachedDestination == true)
            {
                Wait();
            }
        }
    }
}
