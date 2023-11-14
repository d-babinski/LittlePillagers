using UnityEngine;

public class MaintenanceCalculatorFromDatabase : MonoBehaviour
{
    [SerializeField] private UnitDatabase database = null;
    [SerializeField] private UnitTemplateDatabase templates = null;
    [SerializeField] private ResourcesVariable output = null;

    private void Update()
    {
        output.Value = calculateMaintenanceOfDatabase();
    }
    
    private Resources calculateMaintenanceOfDatabase()
    {
        Resources _maintenance = new Resources();
        (UnitType[], int[]) _databaseValues = database.Database;

        for (int i = 0; i < _databaseValues.Item1.Length; i++)
        {
            _maintenance += templates.GetTemplate(_databaseValues.Item1[i]).Maintenance*_databaseValues.Item2[i];
        }

        return _maintenance;
    }
}
