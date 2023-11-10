using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Resources
{
    public int Wood;
    public int Wheat;
    public int Metal;
    public int Gold;

    public Resources(int _wood = 0, int _wheat = 0, int _metal = 0, int _gold = 0)
    {
        Wood = _wood;
        Wheat = _wheat;
        Metal = _metal;
        Gold = _gold;
    }

    public static Resources operator +(Resources _a, Resources _b)
    {
        return new Resources(_a.Wood + _b.Wood, _a.Wheat + _b.Wheat, _a.Metal + _b.Metal, _a.Gold + _b.Gold);
    }

    public static Resources operator -(Resources _a, Resources _b)
    {
        return new Resources(_a.Wood - _b.Wood, _a.Wheat - _b.Wheat, _a.Metal - _b.Metal, _a.Gold - _b.Gold);
    }

    public static Resources operator *(Resources _a, float _b)
    {
        return new Resources(
            Mathf.FloorToInt(_a.Wood*_b),
            Mathf.FloorToInt(_a.Wheat*_b),
            Mathf.FloorToInt(_a.Metal*_b),
            Mathf.FloorToInt(_a.Gold*_b)
        );
    }

    public static Resources operator *(Resources _a, int _b)
    {
        return new Resources(_a.Wood*_b, _a.Wheat*_b, _a.Metal*_b, _a.Gold*_b);
    }

    public static Resources operator *(int _b, Resources _a)
    {
        return new Resources(_a.Wood*_b, _a.Wheat*_b, _a.Metal*_b, _a.Gold*_b);
    }

    public static float operator /(Resources _resources, Resources _cost)
    {
        int _maxWithWood = _cost.Wood > 0 ? _resources.Wood/_cost.Wood : int.MaxValue;
        int _maxWithGold = _cost.Gold > 0 ? _resources.Gold/_cost.Gold : int.MaxValue;
        int _maxWithMetal = _cost.Metal > 0 ? _resources.Metal/_cost.Metal : int.MaxValue;
        int _maxWithWheat = _cost.Wheat > 0 ? _resources.Wheat/_cost.Wheat : int.MaxValue;

        return Mathf.Min(_maxWithWood, _maxWithGold, _maxWithMetal, _maxWithWheat);
    }

    public static Resources RandomBetween(Resources _first, Resources _second)
    {
        int _wood = Random.Range(_first.Wood, _second.Wood + 1);
        int _wheat = Random.Range(_first.Wheat, _second.Wheat + 1);
        int _metal = Random.Range(_first.Metal, _second.Metal + 1);
        int _gold = Random.Range(_first.Gold, _second.Gold + 1);

        return new Resources(_wood, _wheat, _metal, _gold);
    }

    public static bool operator >(Resources _leftSide, Resources _rightSide)
    {
        return _leftSide/_rightSide > 1f;
    }

    public static bool operator <(Resources _leftSide, Resources _rightSide)
    {
        return _leftSide/_rightSide < 1f;
    }

    public static int Sum(Resources _resources)
    {
        return _resources.Wheat + _resources.Wood + _resources.Gold + _resources.Metal;
    }

    public static bool operator ==(Resources _leftSide, Resources _rightSide)
    {
        return _leftSide.Wood == _rightSide.Wood && _leftSide.Wheat == _rightSide.Wheat && _leftSide.Gold == _rightSide.Gold && _leftSide.Metal == _rightSide.Metal;
    }

    public static bool operator !=(Resources _leftSide, Resources _rightSide)
    {
        return !(_leftSide == _rightSide);
    }
}
