using UnityEngine;

public class IslandSpawnAreas : MonoBehaviour
{
    public BoxCollider2D AttackerArea => attackerSpawnArea;
    public BoxCollider2D DefenderArea => defenderSpawnArea;
    
    [SerializeField] private BoxCollider2D attackerSpawnArea = null;
    [SerializeField] private BoxCollider2D defenderSpawnArea = null;

    public Vector2 GetRandomAttackerPosition()
    {
        return attackerSpawnArea.GetRandomPointWithinCollider();
     }

    public Vector2 GetRandomDefenderPosition()
    {
        return defenderSpawnArea.GetRandomPointWithinCollider();
    }
}
