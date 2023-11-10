using DG.Tweening;
using System;
using UnityEngine;

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
    public IntVariable Attack = null;
    public IntVariable Capacity = null;
    public int AttackLeft => attackLeft;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Navigator navigator = null;
    
    private int attackLeft = 0;
    private State currentState = State.Waiting;

    private void Start()
    {
        attackLeft = Attack.Value;
    }

    private void Update()
    {
        if (currentState != State.Moving)
        {
            return;
        }
        
        if (navigator.NextPosition.x > transform.position.x)
        {
            if (navigator.NextPosition.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
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
