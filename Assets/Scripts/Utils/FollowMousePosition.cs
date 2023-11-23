using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMousePosition : MonoBehaviour
{
    public void FixedUpdate()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
    }
}
