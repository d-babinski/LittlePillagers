using UnityEngine;

[CreateAssetMenu]
public class IslandType : ScriptableObject
{
    public string IslandName = "";
    public Sprite IslandSprite = null;
    public Stage[] Stages = null;
}
