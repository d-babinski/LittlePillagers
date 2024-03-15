using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public float MinRange = 0f;
    public float MaxRange = 5f;
    public string AnimationName = "Melee";
    public int Damage = 0;
    public float Cooldown = 0f;
    public float LastUsed = -9999f;

    [SerializeField] protected TargetingSystem targetingSystem = null;
    [SerializeField] protected float duration = 1f;

    public bool IsOffCooldown()
    {
        return Time.time - LastUsed > Cooldown;
    }

    public Vector2 GetTarget(Unit _unit, UnitRuntimeSet _allUnits)
    {
        return targetingSystem.ChooseTarget(_unit, _allUnits).transform.position; //TODO: Figure out if its better to set get position or unit and why
    }
    
    public abstract float  MakeAttack(float _normalizedTime, LayerMask _enemyMask, Vector3 _currentPos,  Vector3 _target, int _damage);
    
    
}
