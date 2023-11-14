﻿using UnityEngine;

public class Soldier : MonoBehaviour
{
    public UnitType UnitType = null;
    public string SoldierName = "";

    private enum State
    {
        Waiting = 0,
        Moving = 1,
        Attacking = 2,
        Dead = 3,
    }

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Animator Animator => animator;
    public bool IsDead => currentState == State.Dead;
    public bool IsAttacking => currentState == State.Attacking;
    public bool IsIdle => currentState == State.Waiting;
    public int Attack = 0;
    public int Capacity = 0;
    public int AttackLeft => attackLeft;
    public bool IsMoving => currentState == State.Moving;
    public Resources HeldResources = new Resources();

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Navigator navigator = null;

    private int attackLeft = 0;
    private State currentState = State.Waiting;

    private void Start()
    {
        attackLeft = Attack;
    }

    private void Update()
    {
        if (currentState != State.Moving)
        {
            return;
        }

        if (navigator.NextPosition.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (navigator.NextPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }

        transform.position = navigator.NextPosition;
    }

    public void MoveTo(Vector3 _pos)
    {
        navigator.MoveTo(_pos);
        animator.SetTrigger("Run");
        currentState = State.Moving;
    }

    public void MakeAttack()
    {
        animator.SetTrigger("Attack");
        currentState = State.Attacking;
    }

    public void TakeDamage(int _amount)
    {
        attackLeft -= _amount;
    }

    public void Die()
    {
        animator.SetTrigger("Death");
        currentState = State.Dead;
    }

    public void Wait()
    {
        animator.SetTrigger("Idle");
        currentState = State.Waiting;
    }
}
