using UnityEngine;

[CreateAssetMenu(menuName = "Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int value = 0;

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
