using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour
{
    public event Action<UnitType> OnDeathEvent = null;

    public enum UnitState
    {
        Idle = 0,
        Move = 1,
        Attack = 2,
        Dead = 3,
    }

    public enum FaceDirection
    {
        Left = 0,
        Right = 1,
    }
    
    public Team UnitTeam = null;
    public Attack CurrentAttack = null;
    public float CurrentAttackProgress = 0f;
    public Unit Target = null;
    public Dictionary<Attack, float> LastUseOfAttacks = new();
    public UnitState CurrentState = UnitState.Idle; 
    
    [FormerlySerializedAs("UnitTemplate")] public UnitType UnitType = null;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] Animator animator = null;
    
    public int SkillBonus = 0;
    private int currentHp = 0;

    private void Start()
    {
        currentHp = UnitType.MaxHp;
        spriteRenderer.sprite = UnitType.InGameSprite;
        animator.runtimeAnimatorController = UnitType.Animator;
        
        foreach (Attack _attack in UnitType.Attacks)
        {
            LastUseOfAttacks[_attack] = Mathf.NegativeInfinity;
        }
    }

    public void GetDamaged(int _damage)
    {
        if (currentHp <= 0)
        {
            return;
        }
        
        currentHp -= _damage;

        if (currentHp <= 0)
        {
            ChangeState(UnitState.Dead);
            OnDeathEvent?.Invoke(UnitType);
        }
    }

    public void Face(FaceDirection _direction)
    {
        spriteRenderer.flipX = _direction != FaceDirection.Right;
    }
    
    public void ChangeState(UnitState _newState)
    {
        if (CurrentState == UnitState.Dead)
        {
            return;
        }
        
        switch (_newState)
        {
            case UnitState.Idle:
                animator.SetTrigger("Idle");
                break;
            case UnitState.Move:
                animator.SetTrigger("Run");
                break;
            case UnitState.Attack:
                animator.SetTrigger(CurrentAttack.AnimationName);
                CurrentAttackProgress = 0f;
                break;
            case UnitState.Dead:
                animator.SetTrigger("Death");
                break;
        }

        CurrentState = _newState;
    }

    public void ProgressCurrentAttack()
    {
        if (CurrentAttackProgress >= 1f)
        {
            return;
        }

        Vector3 _targetPos = Target.transform.position;
        Vector3 _myPos = transform.position;
        
        CurrentAttackProgress = CurrentAttack.MakeAttack(CurrentAttackProgress, UnitTeam.EnemyUnitMask.Value, _myPos, _targetPos, SkillBonus);

        if (CurrentAttackProgress >= 1f)
        {
            LastUseOfAttacks[CurrentAttack] = Time.time;
        }
    }

    public void MoveTo(Vector2 _targetPos)
    {
        Vector2 _newPos =  Vector2.MoveTowards(transform.position, _targetPos, UnitType.Speed * 0.05f * Time.deltaTime);
        transform.position = _newPos;
    }
    
    public void GetAdditionalSkillBonus(int _bonus)
    {
        SkillBonus += _bonus;
    }
    
    public void Heal(int _healthRestored)
    {
        if (currentHp <= 0)
        {
            return;
        }
        
        currentHp = Mathf.Clamp(currentHp + _healthRestored, 0, UnitType.MaxHp);
    }
}
