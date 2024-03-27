using UnityEngine;

public class PlayerStateDependency : MonoBehaviour
{
    [SerializeField] private GameObject controlledObject = null;
    [SerializeField] private PlayerStateVariable playerState = null;
    [SerializeField] private PlayerTargetState targetDependency = PlayerTargetState.NotChosen | PlayerTargetState.Chosen;
    [SerializeField] private PlayerCombatState combatStateDependency = PlayerCombatState.Combat | PlayerCombatState.Preparation;
    [SerializeField] private bool requiresParticularTarget = false;
    [SerializeField] private Island requiredTarget = null;
    
    private void Start()
    { 
        setGameObjectStateForCurrentPlayerState(playerState);
        playerState.StateChanged += onPlayerStateChanged;
    }

    private void OnDestroy()
    {
        playerState.StateChanged -= onPlayerStateChanged;
    }

    private void onPlayerStateChanged()
    {
        setGameObjectStateForCurrentPlayerState(playerState);
    }

    private void setGameObjectStateForCurrentPlayerState(PlayerStateVariable _playerState)
    {
        if( targetDependency.HasFlag(_playerState.TargetState) == false)
        {
            controlledObject.SetActive(false);
            return;
        }
        
        if (combatStateDependency.HasFlag(_playerState.CombatState) == false)
        {
            controlledObject.SetActive(false);
            return;
        }

        if (requiresParticularTarget == true && requiredTarget != _playerState.CurrentTarget)
        {
            controlledObject.SetActive(false);
            return;
        }
        
        controlledObject.SetActive(true);
    }

    public void SetStateDependancy(GameObject _controlledGameObject, PlayerStateVariable _playerState, PlayerCombatState _combatStateDependancy, PlayerTargetState _targetDependency)
    {
        playerState = _playerState;
        combatStateDependency = _combatStateDependancy;
        targetDependency = _targetDependency;
        controlledObject = _controlledGameObject;
    }

    public void SetTargetRequirement(bool _requiresTarget, Island _target)
    {
        requiresParticularTarget = _requiresTarget;
        requiredTarget = _target;
    }
}
