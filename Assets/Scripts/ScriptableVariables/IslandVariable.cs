using UnityEngine;

[CreateAssetMenu(menuName = "Variables/IslandVariable")]
public class IslandVariable : ScriptableObject
{
    [SerializeField] private Island value = null;

    public Island Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
