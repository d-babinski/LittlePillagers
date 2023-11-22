using UnityEngine;

public abstract class MovementTrajectory : ScriptableObject
{
    public abstract Vector2 GetNextPosition(Vector2 _from, Vector2 _to, float _normalizedT);
}
