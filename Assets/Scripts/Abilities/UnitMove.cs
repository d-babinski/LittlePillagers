using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private const float SPEED_SCALING = 0.05f;

    public Vector3 MoveTowards(Vector3 _target, float _speed)
    {
        return Vector2.MoveTowards(transform.position, _target, _speed * SPEED_SCALING * Time.deltaTime);
    }
}
