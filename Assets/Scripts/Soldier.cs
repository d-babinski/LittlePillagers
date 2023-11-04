using DG.Tweening;
using System;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public event Action<Soldier> OnDeath = null;
    public event Action<Soldier> OnReturn = null;
    
    public int TemplateId = 0;
    public string SoldierName = "";

    private enum State
    {
        Waiting = 0,
        Moving = 1,
        Attacking = 2,
        Dead = 3,
    }
    
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public MissionType GivenOrders => orders;
    public Animator Animator => animator;
    public bool IsDead => currentState == State.Dead;
    public int Attack => attack;
    public int AttackLeft => attackLeft;
    public int Capacity => capacity;
    public Vector3 RallyPoint => rallyPoint;
    public bool IsAttacking => currentState == State.Attacking;
    public bool IsIdle => currentState == State.Waiting;
    public Resources Pillaged => pillagedResources;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Navigator navigator = null;
    
    private int attack = 0;
    private int attackLeft = 0;
    private State currentState = State.Waiting;
    private int capacity = 0;
    private Resources pillagedResources = new Resources();
    private MissionType orders = MissionType.Pillage;
    private Vector3 rallyPoint = Vector3.zero;

    private void Awake()
    {
        navigator.OnPointReached += pointReached;
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

    public void SetId(int _templateSoldierId)
    {
        TemplateId = _templateSoldierId;
    }
    
    public void SetSoldierName(string _templateSoldierName)
    {
        SoldierName = _templateSoldierName;
    }

    public void SetCapacity(int _capacity)
    {
        capacity = _capacity;
    }

    public void GiveOrders(MissionType _orders)
    {
        orders = _orders;
    }
    
    public void MoveTo(Vector3 _pos)
    {
        navigator.MoveTo(_pos , 3f);
        animator.SetTrigger("Run");
        currentState = State.Moving;
    }

    public void SetRallyPoint(Vector3 _rallyPoint)
    {
        rallyPoint = _rallyPoint;
    }
    
    private void pointReached()
    {
        Wait();
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
    
    public void SetAttack(int _attack)
    {
        attack = _attack;
        attackLeft = _attack;
    }

    public void Return()
    {
        OnReturn?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void PillageResources(Resources _resources)
    {
        pillagedResources += _resources;
    }
    
    public void Die()
    {
        animator.SetTrigger("Death");
        currentState = State.Dead;
        OnDeath?.Invoke(this);
    }
    
    public void Wait()
    {
        animator.SetTrigger("Idle");
        currentState = State.Waiting;
    }
    
    public void ReturnToRallyPoint()
    {
        navigator.OnPointReached += _returnToRallyPoint;
        MoveTo(RallyPoint);

        void _returnToRallyPoint()
        {
            navigator.OnPointReached -= _returnToRallyPoint;
            Return();
        }
    }
}
