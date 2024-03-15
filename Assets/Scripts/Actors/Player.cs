using CHARK.ScriptableEvents.Events;
using ScriptableEvents.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private IslandScriptableEvent targetChosenEvent = null;
    [SerializeField] private SimpleScriptableEvent targetCanceledEvent = null;
    
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private Army army = null;

    private Island attackTarget = null;
    
    public void AddResources(Resources _res)
    {
        playerResources.Value += _res;
    }

    public void BuyUnits(UnitType _type)
    {
        Resources _unitCost = _type.BaseCost;

        if (playerResources.Value < _unitCost)
        {
            return;
        }

        army.AddUnits(_type);
        playerResources.Value -= _unitCost;
    }

    public void OnUnitDied(UnitType _type)
    {
        army.RemoveUnits(_type);
    }

    public void OnEmbarkPressed()
    {
        if (attackTarget == null || playerStateVariable.CurrentState == PlayerState.Combat)
        {
            return;
        }
        
        playerStateVariable.ChangeState(PlayerState.Combat);
    }
    
    public void TryChoosingNewTarget(Island _target)
    {
        if (attackTarget != null)
        {
            return;
        }

        attackTarget = _target;
        targetChosenEvent.Raise(attackTarget);
    }

    public void TryCancelingCurrentTarget()
    {
        if (attackTarget == null)
        {
            return;
        }

        attackTarget = null;
        targetCanceledEvent.Raise();
    }
}
