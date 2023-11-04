using UnityEngine;

public class BuildingSpot : MonoBehaviour
{
    public bool IsEmpty = false;
    private Building building = null;

    private void Awake()
    {
        building = GetComponentInChildren<Building>();
        IsEmpty = building == null;
    }

    public void BuildBuilding(Building _building)
    {
        building = Instantiate(_building, transform);
        building.transform.localPosition = Vector3.zero;
        IsEmpty = false;
    }

    public Building GetBuilding()
    {
        return building;
    }
}
