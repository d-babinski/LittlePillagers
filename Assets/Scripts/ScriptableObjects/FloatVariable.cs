using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float value = 0f;

    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
