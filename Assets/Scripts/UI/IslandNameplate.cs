using TMPro;
using UnityEngine;

public class IslandNameplate : MonoBehaviour
{
    [SerializeField] private Island trackedIsland = null;
    [SerializeField] private TextMeshProUGUI textComponent = null;

    public void SetContext(Island _context)
    {
        trackedIsland = _context;
        textComponent.text = _context.IslandType.IslandName;
    }

    public Island GetContext()
    {
        return trackedIsland;
    }
}
