using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public UnitTemplate UnitTemplate = null;
    public Unit Target = null;
    public LayerMaskVariable EnemyMask = null;
    public UnityEvent DeathActions = null;
    public UnityEvent<UnitType> DeathUnitTypeActions = null;

    [SerializeField] private UnitRuntimeSet aliveEnemies = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private Navigator navigator = null;
    [SerializeField] private TargetingSystem targetingSystem = null;

    private int damageBonus = 0;
    private int currentHp = 0;
    private float lastSpecialAttackTime =-99f;
    private Attack currentAttack = null;
    private float currentAttackNormalizedT = 0f;

    private void Start()
    {
        currentHp = UnitTemplate.MaxHp;
        spriteRenderer.sprite = UnitTemplate.InGameSprite;
        animator.runtimeAnimatorController = UnitTemplate.Animator;
    }

    private void Update()
    {
        if (currentHp <= 0)
        {
            return;
        }

        if (Target == false || Target.currentHp <= 0)
        {
            Target = targetingSystem.ChooseTarget(transform.position, aliveEnemies.Items);
            return;
        }
        
        if (currentAttack == false && trySelectingNewAttack() == false)
        {
            MoveTo(Target.transform.position);
            return;
        }

        progressAttack(currentAttack);
    } 

    private void progressAttack(Attack _attackToProgress)
    {
        Vector3 _targetPos = Target.transform.position;
        Vector3 _myPos = transform.position;
        int _damage = UnitTemplate.MeleeDamage + damageBonus;
        
        currentAttackNormalizedT = _attackToProgress.MakeAttack(currentAttackNormalizedT, EnemyMask.Value, _myPos, _targetPos, _damage);

        if (currentAttackNormalizedT >= 1f)
        {
            currentAttackNormalizedT = 0f;
            currentAttack = null;
            Target = null;
            animator.SetTrigger("Idle");
        }
    }

    private bool trySelectingNewAttack()
    {
        if (UnitTemplate.SpecialAttack && IsSpecialAttackOffCooldown() && IsInRange(UnitTemplate.SpecialAttack))
        {
            lastSpecialAttackTime = Time.time;
            currentAttack = UnitTemplate.SpecialAttack;
            animator.SetTrigger("Special");
            return true;
        }

        if (UnitTemplate.MeleeAttack && IsInRange(UnitTemplate.MeleeAttack))
        {
            currentAttack = UnitTemplate.MeleeAttack;
            animator.SetTrigger("Melee");
            return true;
        }

        if (UnitTemplate.RangedAttack && IsInRange(UnitTemplate.RangedAttack))
        {
            animator.SetTrigger("Ranged");
            currentAttack = UnitTemplate.RangedAttack;
            return true;
        }

        return false;
    }

    public bool IsSpecialAttackOffCooldown()
    {
        return Time.time > lastSpecialAttackTime + UnitTemplate.SpecialAttCooldown;
    }
    
    public bool IsInRange(Attack _attack)
    {
        if (Target == false)
        {
            return false;
        }

        float _distanceToTarget = Vector2.Distance(transform.position, Target.transform.position);

        return _distanceToTarget < _attack.MaxRange && _distanceToTarget > _attack.MinRange;
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
            DeathActions?.Invoke();
            DeathUnitTypeActions?.Invoke(UnitTemplate.TypeOfUnit);
            animator.SetTrigger("Death");
        }
    }
    
    public void MoveTo(Vector3 _targetPos)
    {
        transform.position = navigator.MoveTo(_targetPos, UnitTemplate.Speed);
    }
    
    public void GetAttackBonus(int _bonus)
    {
        damageBonus += _bonus;
    }
    
    public void Heal(int _healthRestored)
    {
        if (currentHp <= 0)
        {
            return;
        }
        
        currentHp = Mathf.Clamp(currentHp + _healthRestored, 0, UnitTemplate.MaxHp);
    }
}
