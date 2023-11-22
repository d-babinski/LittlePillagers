using UnityEngine;

[CreateAssetMenu(menuName = "Variables/FloatVariable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float value = 0f;

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
