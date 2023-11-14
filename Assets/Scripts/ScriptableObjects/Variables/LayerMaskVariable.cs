using UnityEngine;

[CreateAssetMenu]
public class LayerMaskVariable : ScriptableObject
{
    [SerializeField] private LayerMask value = 0;

    public LayerMask Value
    {
        get { return value; }
        set { this.value = value; }
    }
}

