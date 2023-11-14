using UnityEngine;

[CreateAssetMenu]
public class Vector3Variable : ScriptableObject
{
    [SerializeField] private Vector3 value = new Vector3();

    public Vector3 Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
