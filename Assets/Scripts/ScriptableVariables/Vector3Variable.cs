using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector3Variable")]
public class Vector3Variable : ScriptableObject
{
    [SerializeField] private Vector3 value = new Vector3();

    public Vector3 Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
