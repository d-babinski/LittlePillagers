using UnityEngine;

public class SetSpriteSize : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    public void SetSize(Vector2 _size)
    {
        spriteRenderer.size = _size;
    }
}
