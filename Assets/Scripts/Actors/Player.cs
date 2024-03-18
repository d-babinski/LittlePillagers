using CHARK.ScriptableEvents.Events;
using ScriptableEvents.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private IslandScriptableEvent targetChosenEvent = null;
    [SerializeField] private SimpleScriptableEvent targetCanceledEvent = null;
    [SerializeField] private SimpleScriptableEvent onEmbarkEvent = null;
    
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private Army army = null;

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
        if (playerStateVariable.CurrentTarget == null || playerStateVariable.CombatState == PlayerCombatState.Combat)
        {
            return;
        }
        
        playerStateVariable.ChangeCombatState(PlayerCombatState.Combat);
    }
    
    public void TryChoosingNewTarget(Island _target)
    {
        if (playerStateVariable.CurrentTarget != null)
        {
            return;
        }

        playerStateVariable.ChangeTarget(_target);
        targetChosenEvent.Raise(playerStateVariable.CurrentTarget);
    }

    public void TryCancelingCurrentTarget()
    {
        if (playerStateVariable.CurrentTarget == null)
        {
            return;
        }

        playerStateVariable.ChangeTarget(null);
        targetCanceledEvent.Raise();
    }
}
