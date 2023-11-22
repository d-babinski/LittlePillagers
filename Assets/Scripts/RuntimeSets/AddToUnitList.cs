using UnityEngine;

public class AddToUnitList : MonoBehaviour
{
    public RuntimeSet<Unit> RuntimeSet;

    private void OnEnable()
    {
        if (TryGetComponent(out Unit _unit))
        {
            RuntimeSet.Add(_unit);
        }
    }

    private void OnDisable()
    {
        if (TryGetComponent(out Unit _unit))
        {
            RuntimeSet.Remove(_unit);
        }
    }

}
