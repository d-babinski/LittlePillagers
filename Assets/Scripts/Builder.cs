using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public int Buildings => houses + mines + lighthouses + mills;
    public int Houses => houses;
    public int Mines => mines;
    public int Lighthouses => lighthouses;
    public int Mills => mills;
    public int WallLevel => wallLevel;
    public int SpecialBuildings => lighthouses + mines + mills;

    [SerializeField] private ObjectSpawner houseSpawner = null;
    [SerializeField] private ObjectSpawner millSpawner =  null;
    [SerializeField] private ObjectSpawner mineSpawner =  null;
    [SerializeField] private ObjectSpawner lighthouseSpawner = null;
    [SerializeField] private Wall walls = null;
    
    private Collider2D buildArea = null;

    private int houses = 0;
    private int mines = 0;
    private int lighthouses = 0;
    private int mills = 0;
    private int wallLevel = 0;

    public void BuildHouse()
    {
        houseSpawner.SpawnNewObject();
        houses++;
    }

    public void BuildMine()
    {
        mineSpawner.SpawnNewObject();
        mines++;
    }

    public void BuildLighthouse()
    {
        lighthouseSpawner.SpawnNewObject();
        lighthouses++;
    }

    public void BuildMill()
    {
        millSpawner.SpawnNewObject();
        mills++;
    }
    
    public void BuildWalls(int _level)
    {
        wallLevel = _level;
        walls.ShowWall(_level);
    }

    public void RemoveWalls()
    {
        wallLevel = 0;
        walls.HideWall();
    }
}
