using UnityEngine;

public class SacrificialSword : MonoBehaviour
{
    public UnitRuntimeSet Allies = null;
    
    public Vector2 Range = Vector2.one;
    public LayerMaskVariable HitMask = null;

    [SerializeField] private int selfDamage = 2;

    public float DamageDelay = 0f;

    private bool hasDealtDamage = false;
    private float timeExisting = 0f;

    private void Update()
    {
        if (hasDealtDamage == true)
        {
            return;
        }

        if (timeExisting >= DamageDelay)
        {
            int _bonus = damageAllies();
            Allies.Items.ForEach(_ally => _ally.GetAdditionalSkillBonus(_bonus));
            hasDealtDamage = true;
        }

        timeExisting += Time.deltaTime;
    }

    private int damageAllies()
    {
        Collider2D[] _hits = Physics2D.OverlapBoxAll(transform.position, Range, 0f, HitMask.Value);
        int _hitCount = 0;
            
        foreach (Collider2D _hit in _hits)
        {
            if (_hit.TryGetComponent(out Damageable _damageable))
            {
                _hitCount++;
                _damageable.GetDamaged(selfDamage);
            }
        }

        return _hitCount;
    }
}
