using UnityEngine;

[CreateAssetMenu(menuName = "Variables/UnitTypeVariable")]
public class UnitTypeVariable : ScriptableObject
{
    [SerializeField] private UnitType value = null;

    public UnitType Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
