using UnityEngine;

public class SetPositionFromVariable : MonoBehaviour
{
    public void Set2DPositionFromVariable(Vector3Variable _variable)
    {
        Vector3 _currentPos = transform.position;

        _currentPos.x = _variable.Value.x;
        _currentPos.y = _variable.Value.y;

        transform.position = _currentPos;
    }
}
 