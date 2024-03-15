using UnityEngine;

public class UnitsAI : MonoBehaviour
{
    [SerializeField] private UnitRuntimeSet availableUnits = null;
    [SerializeField] private int relationBitmask = 0;

    private void Update()
    {
        foreach (var _unit in availableUnits.Items)
        {
            if (_unit.IsAlive == false)
            {
                return;
            }

        }
    }
}
