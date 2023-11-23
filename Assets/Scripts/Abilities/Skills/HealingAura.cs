using DG.Tweening;
using UnityEngine;

public class HealingAura : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private float duration = 10f;
    [SerializeField] private float healFrequency = 1f;
    [SerializeField] private int healAmount = 3;
    [SerializeField] private Vector2 areaSize = Vector2.one;
    [SerializeField] private LayerMaskVariable healingMask = null;

    private float startTime = 0f;
    private float lastHeal = 0;
    private bool hasFinished = false;
    
    private void Start()
    {
        spriteRenderer.color = Color.clear;
        spriteRenderer.DOColor(Color.white,0.5f);
        startTime = Time.time;
    }

    private void Update()
    {
        if (hasFinished == true)
        {
            return;
        }

        if (Time.time > lastHeal + healFrequency)
        {
            healUnits();
            lastHeal = Time.time;
        }

        if (Time.time > startTime + duration)
        {
            hasFinished = true;
            spriteRenderer.DOColor(Color.clear, 0.5f);
        }
    }
    
    private void healUnits()
    {
        Collider2D[] _healedUnits = Physics2D.OverlapBoxAll(transform.position, areaSize, healingMask.Value);

        foreach (Collider2D _healedUnit in _healedUnits)
        {
            if (_healedUnit.TryGetComponent(out Unit _unit))
            {
                _unit.Heal(healAmount);
            }
        }
    }
}
