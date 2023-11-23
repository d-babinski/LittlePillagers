using UnityEngine;
using UnityEngine.Events;

public class MovingProjectile : MonoBehaviour
{
    [SerializeField] private Vector3 targetPos = Vector3.zero;
    [SerializeField] private float duration = 3f;

    [SerializeField] private MovementTrajectory trajectory = null;
    [SerializeField] private UnityEvent onReachingPos = null;

    private float timeExecuting = 0f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        timeExecuting = 0f;
    }

    public void SetTarget(Vector2 _target)
    {
        targetPos = _target;
    }

    public void SetDuration(float _duration)
    {
        duration = _duration;
    }
    
    private void Update()
    {
        if (timeExecuting >= duration)
        {
            return;
        }

        timeExecuting += Time.deltaTime;
        
        float _normalizedT = Mathf.Clamp01(timeExecuting/duration);
        
        transform.position = trajectory.GetNextPosition(startPos, targetPos,_normalizedT);

        if (_normalizedT >= 1)
        {
            onReachingPos?.Invoke();
        }
    }
}
