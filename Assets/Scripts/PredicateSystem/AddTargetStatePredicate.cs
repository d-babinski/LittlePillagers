using UnityEngine;

[RequireComponent(typeof(PredicateBasedActions))]
public class AddTargetStatePredicate : MonoBehaviour
{
    [SerializeField] private PlayerStateVariable playerState = null;
    [SerializeField] private PlayerTargetState targetState = PlayerTargetState.Chosen;

    private PredicateBasedActions predicateActions = null;

    private void Awake()
    {
        predicateActions = GetComponent<PredicateBasedActions>();
        predicateActions.AddPredicate(new PlayerTargetStatePredicate(playerState, targetState));
    }
}
