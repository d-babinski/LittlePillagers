using UnityEngine;

public abstract class Attack : ScriptableObject
{
    public float MinRange = 0f;
    public float MaxRange = 5f;
    [SerializeField] protected float duration = 1f;

    public abstract float MakeAttack(float _normalizedTime, LayerMask _enemyMask, Vector3 _currentPos,  Vector3 _target, int _damage);
}
