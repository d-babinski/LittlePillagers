using Dreamteck.Splines;
using UnityEngine;
using Random = UnityEngine.Random;

public static class SplineUtils
{
    public static SplineComputer CreateCurvySpline(SplineComputer _splinePrefab, Vector2 _start, Vector2 _end, int _pointCount, float _maxYVariance = 1.5f)
    {
        SplineComputer _spline = Object.Instantiate(_splinePrefab);
        SplinePoint[] _points = new SplinePoint[_pointCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = new SplinePoint();
            _points[i].position = Vector2.Lerp(_start, _end, (float)i/(_points.Length - 1)) + Vector2.up*((i == 0 || i == _points.Length - 1) ? 0 : Random.Range(-_maxYVariance, _maxYVariance));
            _points[i].normal = Vector3.up;
            _points[i].size = 1f/16f;
            _points[i].color = Color.white;
        }

        _spline.SetPoints(_points);

        return _spline;
    }
}
