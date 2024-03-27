    using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Attack : ScriptableObject
{
    public float MinRange = 0f;
    public float MaxRange = 5f;
    public string AnimationName = "Melee";
    [FormerlySerializedAs("Damage")] public int Power = 0;
    public float Cooldown = 0f;

    [SerializeField] protected TargetingSystem targetingSystem = null;
    [SerializeField] protected float duration = 1f;

    public bool IsOffCooldown(float _lastUsed)
    {
        return Time.time - _lastUsed > Cooldown;
    }

    public Unit GetTarget(Unit _unit, List<Unit> _allUnits)
    {
        return targetingSystem.ChooseTarget(_unit, _allUnits);
    }
    
    public abstract float  MakeAttack(float _normalizedTime, LayerMask _enemyMask, Vector3 _currentPos,  Vector3 _target, int _bonusPower);

    public bool IsTooClose(Vector2 _from, Vector2 _target)
    {
        return Vector2.Distance(_from, _target) < MinRange;
    }

    public bool IsTooFar(Vector2 _from, Vector2 _target)
    {
        return Vector2.Distance(_from, _target) > MaxRange;
    }
    
    public bool IsInRange(Vector2 _from, Vector2 _target)
    {
        float _distanceToTarget = Vector2.Distance(_from,_target);
        return _distanceToTarget < MaxRange && _distanceToTarget > MinRange;
    }
}
