using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Isle : MonoBehaviour
{
    private const int POPULATION_BASE_PRODUCTION = 2;
    public event Action OnMissionResolved = null;

    public string IsleName => isleName;
    public Resources CurrentResources => silo.CurrentResources;
    public int ArmyCount => army.GetTotalSoldierCount();
    public int PopulationCount => populationCount;

    [SerializeField] private int maxBuildings = -1;
    [SerializeField] private string isleName = "Borg";
    [SerializeField] private ResourceSilo silo = null;
    [SerializeField] private NaturalResources resourceSpawner = null;
    [SerializeField] private Army army = null;
    [SerializeField] private Builder builder = null;
    [SerializeField] private SoldierBuilder soldierBuilder = null;
    [SerializeField] private SoldierTemplate template = null;
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
    private int populationCount = 0;

    public void ProduceResources()
    {
        float _armyToPopulationRatio = ArmyCount/PopulationCount;
        float _rocksBonus = 0.25f*rocks;
        float _treesBonus = 0.25f*trees;
        float _wheatBonus = 0.25f*wheat;
        float _goldBonus = (_rocksBonus + _treesBonus + _wheatBonus)/3f;

        int _metalProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_rocksBonus + 1f)*PopulationCount*POPULATION_BASE_PRODUCTION);
        int _woodProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_treesBonus + 1f)*PopulationCount*POPULATION_BASE_PRODUCTION);
        int _wheatProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_wheatBonus + 1f)*PopulationCount*POPULATION_BASE_PRODUCTION);
        int _goldProduced = Mathf.FloorToInt(_armyToPopulationRatio*(_goldBonus + 1f)*PopulationCount*POPULATION_BASE_PRODUCTION);

        silo.AddResources(new Resources(_woodProduced, _wheatProduced, _metalProduced, _goldProduced));
    }

    public Battlefield GetAttacked(Mission _missionType)
    {
        if (currentBattlefield == true && currentBattlefield.IsResolved == false)
        {
            return currentBattlefield;
        }

        currentBattlefield = (new GameObject()).AddComponent<Battlefield>();
        currentBattlefield.OnBattlefieldConcluded += applyResults;
        
        if (_missionType.Type == MissionType.Beg)
        {
            return currentBattlefield;
        }

        if (_missionType.Type == MissionType.Threaten && Random.Range(0f, 1f) > 0.5f)
        {
            return currentBattlefield;
        }

        int _soldiersToMake = ArmyCount;

        for (int i = 0; i < _soldiersToMake; i++)
        {
            Soldier _soldier = soldierBuilder.CreateSoldier(template);
            army.SendSoldiersOnMission(_soldier.TemplateId, 1);
            _soldier.OnDeath += killASoldier;
            _soldier.OnReturn += returnSoldier;
            Vector3 _spawnPoint = possibleUnitSpawns.GetRandom();
            _soldier.transform.position = _spawnPoint;
            _soldier.SetRallyPoint(_spawnPoint);
            _soldier.GiveOrders(MissionType.Defend);
            currentBattlefield.AddAIUnit(_soldier);
        }

        return currentBattlefield;
    }
    private void returnSoldier(Soldier _soldier)
    {
        army.ReturnSoldierFromMission(_soldier.TemplateId);
    }

    private void killASoldier(Soldier _soldier)
    {
        killPop(1);
        army.KillSoldierOnMission(_soldier.TemplateId);
    }
    
    private void killPop(int _amount)
    {
        populationCount = Mathf.Clamp(populationCount-_amount, 0, populationCount);
    }

    private void applyResults(List<Soldier> _soldiersLeft)
    {
        _soldiersLeft.ForEach(_soldier =>
        {
            switch (_soldier.GivenOrders)
            {
                case MissionType.Defend:
                    break;
                case MissionType.Pillage:
                case MissionType.Threaten:
                    _soldier.PillageResources(generatePillagedResources(_soldier.Capacity));
                    break;
                case MissionType.Beg:
                    _soldier.PillageResources(generatePillagedResources(_soldier.Capacity/3));
                    break;
                case MissionType.Destroy:
                    _soldier.PillageResources(generatePillagedResources(_soldier.Capacity));
                    killPop(_soldier.AttackLeft);
                    break;
            }
        });
        
        OnMissionResolved?.Invoke();
    }

    private Resources generatePillagedResources(int _soldierCapacity)
    {
        int _totalRes = CurrentResources.Gold + CurrentResources.Metal + CurrentResources.Wheat + CurrentResources.Wood;

        if (_totalRes < _soldierCapacity)
        {
            Resources _allResources = CurrentResources;
            silo.RemoveResources(CurrentResources);
            return _allResources;
        }
        
        float _woodRatio = (float)CurrentResources.Wood/_totalRes;
        float _wheatRatio = (float)CurrentResources.Wheat/_totalRes;
        float _metalRatio = (float)CurrentResources.Metal/_totalRes;
        float _goldRatio = (float)CurrentResources.Gold/_totalRes;

        Resources _plundered = new Resources(
            Mathf.FloorToInt(_soldierCapacity*_woodRatio),
            Mathf.FloorToInt(_soldierCapacity*_wheatRatio),
            Mathf.FloorToInt(_soldierCapacity*_metalRatio),
            Mathf.FloorToInt(_soldierCapacity*_goldRatio)
        );

        silo.RemoveResources(_plundered);
        
        return _plundered;
    }

    public void SetIslandName(string _name)
    {
        isleName = _name;
    }

    public void SetInitialPopulation(int _population)
    {
        populationCount = _population;
        highestPopulation = _population;
    }

    public void SetInitialResources(Resources _resources)
    {
        silo.AddResources(_resources);
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
        if (populationCount <= 0)
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
        if (PopulationCount < highestPopulation)
        {
            armyRatio = Mathf.MoveTowards(armyRatio, maxArmyRatio, 0.02f);
            return;
        }

        armyRatio = Mathf.MoveTowards(armyRatio, minArmyRatio, 0.005f);
    }


    public void RaiseArmy()
    {
        int _lackingSoldiers = Mathf.FloorToInt(populationCount*armyRatio);

        if (_lackingSoldiers <= 0)
        {
            return;
        }

        army.AddSoldiers(template.SoldierId, _lackingSoldiers);
    }

    private void BuildBuildings()
    {
        if (maxBuildings > 0 && builder.Buildings > maxBuildings)
        {
            return;
        }

        if (1 + populationCount/10 <= builder.Buildings)
        {
            return;
        }

        builder.BuildWalls(ArmyCount/25);

        if (PopulationCount/10 < wheat && armyRatio < 0.75f)
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
        else if (builder.Lighthouses < populationCount/50)
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
        populationCount += Random.Range(1, 2+PopulationCount/10);
    }

    public void BuildInitialBuildings()
    {
        for (int i = 0; i <= PopulationCount/10; i++)
        {
            BuildBuildings();
        }
    }
}
