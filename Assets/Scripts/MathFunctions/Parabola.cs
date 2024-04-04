using UnityEngine;

[CreateAssetMenu(menuName = "Math/Functions/Trajectory/Parabola")]
public class Parabola : MovementTrajectory
{
    [SerializeField] private float arcHeight = 3f;

    public override Vector2 GetNextPosition(Vector2 _from, Vector2 _to, float _normalizedT)
    {
        float _x0 = _from.x;
        float _x1 = _to.x;
        float _dist = _x1 - _x0;
        float _nextX = Mathf.Lerp(_x0, _x1, _normalizedT);
        float _baseY = Mathf.Lerp(_from.y, _to.y, (_nextX - _x0)/_dist);
        float _arc = arcHeight*(_nextX - _x0)*(_nextX - _x1)/(-0.25f*_dist*_dist);
        Vector2 _nextPos = new Vector2(_nextX, _baseY + _arc);

        return _nextPos;
    }
}
