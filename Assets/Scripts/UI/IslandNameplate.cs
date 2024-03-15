using TMPro;
using UnityEngine;

public class IslandNameplate : MonoBehaviour
{
    [SerializeField] private Island trackedIsland = null;
    [SerializeField] private TextMeshPro textComponent = null;

    public void SetContext(Island _context)
    {
        trackedIsland = _context;
        textComponent.text = _context.IslandName;
    }

    public Island GetContext()
    {
        return trackedIsland;
    }
}
