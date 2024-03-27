using System;
using UnityEngine;

[CreateAssetMenu(fileName = "stage_", menuName = "Content/Stage", order = 0)]
public class Stage : ScriptableObject
{
    [System.Serializable]
    public struct StageUnits
    {
        public UnitType UnitType;
        public int Count;
    }

    public StageUnits[] UnitsInStage = Array.Empty<StageUnits>();
    public Resources Rewards = new Resources();
    public Sprite StageIcon = null;
    public RuntimeAnimatorController StageIconAnimator = null;
}
