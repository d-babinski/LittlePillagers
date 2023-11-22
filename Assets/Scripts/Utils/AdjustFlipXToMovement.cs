using UnityEngine;

public class AdjustFlipXToMovement : MonoBehaviour
{
    [SerializeField] private bool resetOnNoMovement = false;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private Vector3 lastPos = Vector3.zero;

    private void Update()
    {
        if (resetOnNoMovement == true || Vector2.Distance(lastPos, transform.position) > Mathf.Epsilon)
        {
            spriteRenderer.flipX = lastPos.x > transform.position.x;
        }
        
        lastPos = transform.position;
    }
}
