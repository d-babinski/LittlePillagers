using UnityEngine;
using UnityEngine.EventSystems;

public class SetUnitTypeVariableOnPointerEnter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnitTypeVariable dataOutput;
    [SerializeField] private UnitType unitTypeSet;

    public void OnPointerEnter(PointerEventData _eventData)
    {
        dataOutput.Value = unitTypeSet;
    }
}
