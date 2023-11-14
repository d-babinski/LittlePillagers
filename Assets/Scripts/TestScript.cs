using Missions;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private UnitType typeToSpawn = null;
    [SerializeField] private UnitType soldierToSpawn = null;
    [SerializeField] private Mission mission = null;
    [SerializeField] private UnitTemplateDatabase database = null;
    
    private void Start()
    {
        mission.Ships.Add(database.GetTemplate(typeToSpawn).InstantiateObject().GetComponent<Ship>());
        mission.Soldiers.Add(database.GetTemplate(soldierToSpawn).InstantiateObject().GetComponent<Soldier>());
        mission.Soldiers.Add(database.GetTemplate(soldierToSpawn).InstantiateObject().GetComponent<Soldier>());
        mission.Soldiers.Add(database.GetTemplate(soldierToSpawn).InstantiateObject().GetComponent<Soldier>());
    }
}
