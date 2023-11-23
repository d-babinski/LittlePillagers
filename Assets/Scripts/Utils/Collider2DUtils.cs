using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Collider2DUtils
{
    public static Vector2 GetRandomPointWithinCollider(this Collider2D _col)
    {
        //even with very small collider to bounding box ratio it should take up to 20 tries
        int _attemptsLeft = 100;
        Vector2 _point = RandomPointWithinBounds(_col.bounds);

        while (_attemptsLeft > 0 && _col.OverlapPoint(_point) == false)
        {
            _attemptsLeft--;
            _point = RandomPointWithinBounds(_col.bounds);
        }

        return _attemptsLeft > 0 ? _point : _col.ClosestPoint(_point);
    }

    public static Vector2 RandomPointWithinBounds(Bounds _bounds)
    {
        return new Vector2(Random.Range(_bounds.min.x, _bounds.max.x), Random.Range(_bounds.min.y, _bounds.max.y));
    }
}
