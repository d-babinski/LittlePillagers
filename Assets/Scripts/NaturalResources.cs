using UnityEngine;

public class NaturalResources : MonoBehaviour
{
    [SerializeField] private ObjectSpawner rocks = null;
    [SerializeField] private ObjectSpawner trees = null;
    [SerializeField] private ObjectSpawner wheat = null;

    public void SetRocks(int _count)
    {
        rocks.SetNumberOfSpawnedObjects(_count);
    }

    public void SetTrees(int _count)
    {
        trees.SetNumberOfSpawnedObjects(_count);   
    }

    public void SetWheat(int _count)
    {
        wheat.SetNumberOfSpawnedObjects(_count);
    }
}
