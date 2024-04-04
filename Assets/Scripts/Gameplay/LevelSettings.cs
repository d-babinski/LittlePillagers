using System;
using UnityEngine;

[CreateAssetMenu]
public class LevelSettings : ScriptableObject
{
    public IslandType[] Islands = Array.Empty<IslandType>();
    public Level.WinCondition WinCondition = Level.WinCondition.BeatAllStages;
}
