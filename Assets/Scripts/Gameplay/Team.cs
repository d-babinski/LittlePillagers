using UnityEngine;

[CreateAssetMenu]
public class Team : ScriptableObject
{
    [Layer] public int UnitLayer = 0;
    public LayerMaskVariable EnemyUnitMask = null;
    public LayerMaskVariable AllyUnitMask = null;
}
