using UnityEngine;
using UnityEngine.EventSystems;

public class SetUnitTypeVariableOnPointerEnter : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private UnitTypeVariable dataOutput = null;
    [SerializeField] private UnitTypeVariable unitTypeToSet = null;

    public void OnPointerEnter(PointerEventData _eventData)
    {
        dataOutput.Value = unitTypeToSet.Value;
    }
}
