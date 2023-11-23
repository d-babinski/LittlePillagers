using UnityEngine;
using UnityEngine.Events;

public class MoveUnitsThenDisable : MonoBehaviour
{
    [Header("Controlled Set")]
    [SerializeField] private UnitRuntimeSet controlledUnits = null;

    [Header("Ship packing settings")]
    [SerializeField] private float distanceToDisable = 0.1f;
    [SerializeField] private Transform targetTransform = null;
    
    [Header("Post return events")]
    [SerializeField] private UnityEvent allUnitsPackedActions = null;
    
    private bool returningUnits = false;

    public void MoveUnitsToDestination()
    {
        returningUnits = true;
    }
    
    private void Update()
    {
        if (returningUnits == false)
        {
            return;
        }

        if (areAllObjectsDisabled() == true)
        {
            returningUnits = false;
            allUnitsPackedActions?.Invoke();   
            return;
        }

        Vector2 _shipPos = targetTransform.transform.position;
        
        for (int i = controlledUnits.Items.Count -1; i >= 0; i--)
        {
            Vector2 _unitPos = controlledUnits.Items[i].transform.position;
            float _distanceToShip = Vector2.Distance(_shipPos, _unitPos);

            if (_distanceToShip <= distanceToDisable)
            {
                controlledUnits.Items[i].gameObject.SetActive(false);
                continue;
            }

            controlledUnits.Items[i].MoveTo(targetTransform.position);
        }
    }

    private bool areAllObjectsDisabled()
    {
        for (var i = 0; i < controlledUnits.Items.Count; i++)
        {
            if(controlledUnits.Items[i].gameObject.activeInHierarchy == true)
            {
                return false;
            }
        }

        return true;
    }
}
