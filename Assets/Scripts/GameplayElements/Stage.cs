using JetBrains.Annotations;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "stage_", menuName = "Content/Stage", order = 0)]
public class Stage : ScriptableObject
{
    [System.Serializable]
    public struct Units
    {
        public UnitType UnitType;
        public int Count;
    }

    public bool IsBeaten { get => isBeaten; set => isBeaten = value; }
    
    public Units[] UnitsInStage = Array.Empty<Units>();
    public Resources Rewards = new Resources();
    public Sprite StageIcon = null;
    public RuntimeAnimatorController StageIconAnimator = null;
    
    private bool isBeaten = false;
}
