using UnityEngine;

public class SetVectorVariableToTransformValue : MonoBehaviour
{
    [SerializeField] private Vector3Variable variableToSet = null;

    public void SetToValue(Transform _transform)
    {
        variableToSet.Value = _transform.position;
    }
}
