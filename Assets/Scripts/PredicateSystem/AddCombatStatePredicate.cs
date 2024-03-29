
using UnityEngine;


[RequireComponent(typeof(PredicateBasedActions))]
public class AddCombatStatePredicate : MonoBehaviour
{
    [SerializeField] private PlayerStateVariable playerState = null;
    [SerializeField] private PlayerCombatState combatState = PlayerCombatState.Preparation;

    private PredicateBasedActions predicateActions = null;
    
    private void Awake()
    {
        predicateActions = GetComponent<PredicateBasedActions>();
        predicateActions.AddPredicate(new PlayerCombatStatePredicate(playerState, combatState));
    }
}
