using UnityEngine;

[CreateAssetMenu(menuName = "Variables/BoolVariable")]
public class BoolVariable : ScriptableObject
{
    [SerializeField] private bool value = false;

    public bool Value
    {
        get { return value; }
        set { this.value = value;}
    }
}
