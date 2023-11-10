using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Isle : MonoBehaviour
{
    private const int POPULATION_BASE_PRODUCTION = 2;
    public event Action OnMissionResolved = null;

    [HideInInspector] public StringVariable IsleName = null;
    [HideInInspector] public ResourcesVariable CurrentResources = null;
    [HideInInspector] public IntVariable ArmyCount = null;
    [HideInInspector] public IntVariable PopulationCount = null;

    [SerializeField] private int maxBuildings = -1;
    [SerializeField] private NaturalResources resourceSpawner = null;
    [SerializeField] private Builder builder = null;
    [SerializeField] private SoldierBuilder soldierBuilder = null;
    [SerializeField] private UnitTemplate template = null;
    [SerializeField] private PointSet possibleUnitSpawns = null;
    [SerializeField] private PointSet dockingPoints = null;

    private Battlefield currentBattlefield = null;

    private float armyRatio = 0.5f;
    private float minArmyRatio = 0.25f;
    private float maxArmyRatio = 1f;

    private int housesBeforeSpecialBuilding = 2;

    private int rocks = 0;
    private int wheat = 0;
    private int trees = 0;

    private int highestPopulation = 0;

    private void Awake()
    {
        PopulationCount = ScriptableObject.CreateInstance<IntVariable>();
        ArmyCount = ScriptableObject.CreateInstance<IntVariable>();
        IsleName = ScriptableObject.CreateInstance<StringVariable>();
        CurrentResources = ScriptableObject.CreateInstance<ResourcesVariable>();
    }

    public void ProduceResources()
    {
        float _armyToPopulationRatio = (float)ArmyCount.Value/PopulationCount.Value;
        float _rocksBonus = 0.25f*rocks;
        float _treesBonus = 0.25f*trees;
        float _wheatBonus = 0.25f*wheat;
        float _goldBonus = (_rocksBonus + _treesBonus + _wheatBonus)/3f;

        int _metalProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_rocksBonus + 1f)*PopulationCount.Value*POPULATION_BASE_PRODUCTION);
        int _woodProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_treesBonus + 1f)*PopulationCount.Value*POPULATION_BASE_PRODUCTION);
        int _wheatProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_wheatBonus + 1f)*PopulationCount.Value*POPULATION_BASE_PRODUCTION);
        int _goldProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_goldBonus + 1f)*PopulationCount.Value*POPULATION_BASE_PRODUCTION);

        CurrentResources.Value += new Resources(_woodProduced, _wheatProduced, _metalProduced, _goldProduced);
    }

    private void returnSoldier(Soldier _soldier)
    {
        ArmyCount.Value++;
        PopulationCount.Value++;
    }

    private void killASoldier(Soldier _soldier)
    {
        PopulationCount.Value--;
        ArmyCount.Value--;
    }
    
    private void killPop(int _amount)
    {
        PopulationCount.Value-=_amount;
    }
    

    private Resources generatePillagedResources(int _soldierCapacity)
    {
        int _totalRes = Resources.Sum(CurrentResources.Value);

        if (_totalRes < _soldierCapacity)
        {
            Resources _allResources = CurrentResources.Value;
            CurrentResources.Value = new Resources();
            return _allResources;
        }
        
        float _woodRatio = (float)CurrentResources.Value.Wood/_totalRes;
        float _wheatRatio = (float)CurrentResources.Value.Wheat/_totalRes;
        float _metalRatio = (float)CurrentResources.Value.Metal/_totalRes;
        float _goldRatio = (float)CurrentResources.Value.Gold/_totalRes;

        Resources _plundered = new Resources(
            Mathf.FloorToInt(_soldierCapacity*_woodRatio),
            Mathf.FloorToInt(_soldierCapacity*_wheatRatio),
            Mathf.FloorToInt(_soldierCapacity*_metalRatio),
            Mathf.FloorToInt(_soldierCapacity*_goldRatio)
        );

        CurrentResources.Value -= _plundered;
        
        return _plundered;
    }
    
    public Vector3 ClosestDockingPoint(Vector3 _position)
    {
        return dockingPoints.GetRandom(); //TODO: implement get closest
    }
    public void SetNaturalResources(int _trees, int _rocks, int _wheat)
    {
        trees = _trees;
        rocks = _rocks;
        wheat = _wheat;

        resourceSpawner.SetTrees(_trees);
        resourceSpawner.SetRocks(_rocks);
        resourceSpawner.SetWheat(_wheat);
    }

    public void UpdateCycle()
    {
        if (PopulationCount.Value <= 0)
        {
            return;
        }

        ProduceResources();
        RaisePopulation();
        BuildBuildings();
        ChangeArmyRatio();
        RaiseArmy();
    }

    private void ChangeArmyRatio()
    {
        if (PopulationCount.Value < highestPopulation)
        {
            armyRatio = Mathf.MoveTowards(armyRatio, maxArmyRatio, 0.02f);
            return;
        }

        armyRatio = Mathf.MoveTowards(armyRatio, minArmyRatio, 0.005f);
    }


    public void RaiseArmy()
    {
        int _lackingSoldiers = Mathf.FloorToInt(PopulationCount.Value*armyRatio);

        if (_lackingSoldiers <= 0)
        {
            return;
        }

        ArmyCount.Value += _lackingSoldiers;
    }

    private void BuildBuildings()
    {
        if (maxBuildings > 0 && builder.Buildings > maxBuildings)
        {
            return;
        }

        if (1 + PopulationCount.Value/10 <= builder.Buildings)
        {
            return;
        }

        builder.BuildWalls(ArmyCount.Value/25);

        if (PopulationCount.Value/10 < wheat && armyRatio < 0.75f)
        {
            wheat++;
            resourceSpawner.SetWheat(wheat);
        }

        if (builder.SpecialBuildings > builder.Houses/housesBeforeSpecialBuilding)
        {
            builder.BuildHouse();
        }
        else if (wheat > 2 && builder.Mills <= wheat/6)
        {
            builder.BuildMill();
        }
        else if (rocks > 2 && builder.Mines <= rocks/5)
        {
            builder.BuildMine();
        }
        else if (builder.Lighthouses < PopulationCount.Value/50)
        {
            builder.BuildLighthouse();
        }
        else
        {
            builder.BuildHouse();
        }
    }

    private void RaisePopulation()
    {
        PopulationCount.Value += Random.Range(1, 2+PopulationCount.Value/10);
    }

    public void BuildInitialBuildings()
    {
        for (int i = 0; i <= PopulationCount.Value/10; i++)
        {
            BuildBuildings();
        }
    }
}
