using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Attacks/SimpleShoot")]
public class ShootProjectile : Attack
{
    [FormerlySerializedAs("projectileToSpawn")][FormerlySerializedAs("projectileSToSpawn")][SerializeField] private MovingProjectile movingProjectileToSpawn = null;
    [SerializeField] private float attackRadius = 0.2f; 
    [SerializeField] private float spawnProjectileMoment = 0.5f;
    
    public override float MakeAttack(float _normalizedTime, LayerMask _enemyMask, Vector3 _currentPos, Vector3 _target, int _bonusPower)
    {
        float _newTime = _normalizedTime + Time.deltaTime/duration;
        
        if (_newTime >= spawnProjectileMoment && _normalizedTime < spawnProjectileMoment)
        {
            MovingProjectile _spawnedMovingProjectile = Instantiate(movingProjectileToSpawn);
            _spawnedMovingProjectile.transform.position = _currentPos;
            _spawnedMovingProjectile.SetDuration((1-_normalizedTime) * duration);
            _spawnedMovingProjectile.SetTarget(_target);
        }

        if (_newTime >= 1f && _normalizedTime < 1f)
        {
            Collider2D[] _hits = Physics2D.OverlapCircleAll(_target, attackRadius, _enemyMask);
            
            foreach (Collider2D _collider2D in _hits)
            {
                if (_collider2D.TryGetComponent(out Damageable _damageable))
                {
                    _damageable.GetDamaged(Power + _bonusPower);
                }
            }
        }

        return _newTime;
    }
}
