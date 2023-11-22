using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/SimpleMelee")]
public class MeleeAttack : Attack
{
    [SerializeField] private float attackRadius = 0.2f;
    [SerializeField] private float attackDistance = 0.2f;
    [SerializeField] private float attackNormalizedTime = 0.5f;
    
    public override float MakeAttack(float _normalizedTime, LayerMask _enemyMask, Vector3 _currentPos, Vector3 _target, int _damage)
    {
        float _newTime = _normalizedTime + Time.deltaTime/duration;
        
        if (_newTime >= attackNormalizedTime && _normalizedTime < attackNormalizedTime)
        {
            Vector2 _direction = (_target - _currentPos).normalized;
            RaycastHit2D[] _hits = Physics2D.CircleCastAll(_currentPos, attackRadius, _direction, attackDistance, _enemyMask);
            
            foreach (RaycastHit2D _raycastHit2D in _hits)
            {
                if (_raycastHit2D.collider.TryGetComponent(out Damageable _damageable))
                {
                    _damageable.GetDamaged(_damage);
                }
            }
        }
        

        return _newTime;
    }
}
