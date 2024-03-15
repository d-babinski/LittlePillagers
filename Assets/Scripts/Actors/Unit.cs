using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour
{
    public int Team = 0;
    public bool IsAlive = true;
    public bool IsInCombat = true;
    
    public Vector2 MovementTarget = Vector2.zero;
    public Vector2 ClosestEnemy = Vector2.zero;
    
    public Attack[] Attacks = null;
    
    [FormerlySerializedAs("UnitTemplate")] public UnitType UnitType = null;
    public Unit Target = null;
    public UnityEvent DeathActions = null;
    public UnityEvent<UnitType> DeathUnitTypeActions = null;

    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private Animator animator = null;
    [FormerlySerializedAs("navigator")][SerializeField] private UnitMove unitMove = null;
    [SerializeField] private TargetingSystem targetingSystem = null;
    
    public int DamageBonus = 0;
    private int currentHp = 0;
    private Attack currentAttack = null;
    private float currentAttackNormalizedT = 0f;

    private void Start()
    {
        currentHp = UnitType.MaxHp;
        spriteRenderer.sprite = UnitType.InGameSprite;
        animator.runtimeAnimatorController = UnitType.Animator;
    }

    private void Update()
    {
        if (currentHp <= 0)
        {
            return;
        }

        if (currentAttack == false && selectNewAttack() == false)
        {
            MoveTo(Target.transform.position);
            return;
        }

        progressAttack(currentAttack);
    }

    public void SetAttacks(Attack[] _attacks)
    {
        Attacks = new Attack[_attacks.Length];

        for (int i = 0; i < _attacks.Length; i++)
        {
            Attacks[i] = (Attack)ScriptableObject.CreateInstance(_attacks[i].GetType());
        }
    }

    private void progressAttack(Attack _attackToProgress)
    {
        Vector3 _targetPos = Target.transform.position;
        Vector3 _myPos = transform.position;
        
        //TODO : Think of a way to establish enemy mask, probably some scriptable describing relations via flags? Maybe custom editor?
        currentAttackNormalizedT = _attackToProgress.MakeAttack(currentAttackNormalizedT, 512, _myPos, _targetPos, DamageBonus);

        if (currentAttackNormalizedT >= 1f)
        {
            currentAttack.LastUsed = Time.time;
            currentAttackNormalizedT = 0f;
            currentAttack = null;
            Target = null;
            animator.SetTrigger("Idle");
        }
    }

    private bool selectNewAttack()
    {
        if (Attacks.Length <= 0)
        {
            return false;
        }
        
        int _selectedAttackId = Random.Range(0, Attacks.Length);
        Attack _selectedAttack = Attacks[_selectedAttackId];

        if (_selectedAttack.IsOffCooldown() == false)
        {
            return false;
        }

        if (IsInRange(_selectedAttack) == false)
        {
            return false;
        }

        currentAttack = _selectedAttack;
        animator.SetTrigger(_selectedAttack.AnimationName);
        return true;
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
            DeathUnitTypeActions?.Invoke(UnitType);
            animator.SetTrigger("Death");
        }
    }
    
    public void MoveTo(Vector3 _targetPos)
    {
        transform.position = unitMove.MoveTowards(_targetPos, UnitType.Speed);
    }
    
    public void GetAttackBonus(int _bonus)
    {
        DamageBonus += _bonus;
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
