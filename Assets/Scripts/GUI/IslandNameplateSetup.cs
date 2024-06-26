using UnityEngine;

[CreateAssetMenu]
public class IslandNameplateSetup : ScriptableObject
{
    [SerializeField] private IslandNameplate nameplatePrefab = null;
    [SerializeField] private Vector2 offset = Vector2.zero;

    public IslandNameplate CreateNameplateForIsland(Island _context, Transform _parent)
    {
        IslandNameplate _createdNameplate = Instantiate(nameplatePrefab, _parent);
        
        _createdNameplate.SetContext(_context);
        _createdNameplate.transform.position = (Vector2)_context.transform.position + offset;
        return _createdNameplate;
    }
}
