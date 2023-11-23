using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int> OnDamagedActions = null;
    
    public void GetDamaged(int _damage)
    {
        OnDamagedActions?.Invoke(_damage);
    }
}
