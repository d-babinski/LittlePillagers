using CHARK.ScriptableEvents.Events;
using DefaultNamespace;
using ScriptableEvents.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private IslandScriptableEvent targetChosenEvent = null;
    [SerializeField] private SimpleScriptableEvent targetCanceledEvent = null;
    [SerializeField] private SimpleScriptableEvent onEmbarkEvent = null;
    [SerializeField] private SimpleScriptableEvent playerLostEvent = null;

    [SerializeField] private PlayerSkillCaster playerSkillCaster = null;
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private Army army = null;
    [SerializeField] private IslandPaths pathsToIslands = null;
    [SerializeField] private PlayerShip ship = null;
    [SerializeField] private ClickableResourceSpawner resourceSpawner = null;
    
    [SerializeField] private Team playerTeam = null;
    [SerializeField] private Team enemyTeam = null;

    private bool waitingForReturnToShip = false;
    private Battle currentBattle = null;
    
    private void OnEnable()
    {
        playerSkillCaster.OnSkillCastEvent += removeSpellCost;
        ship.OnArrival += onShipArrival;
    }

    private void OnDisable()
    {
        playerSkillCaster.OnSkillCastEvent -= removeSpellCost;
        ship.OnArrival -= onShipArrival;
    }

    private void Update()
    {
        if (waitingForReturnToShip == true)
        {
            if (MoveToShipAI.MoveAliveUnitsToPointAndDestroy(currentBattle.AttackerUnits, ship.transform.position) == MoveToShipAI.State.AllUnitsMoved)
            {
                resourceSpawner.SpawnResources(playerStateVariable.CurrentTarget.GetCurrentStage().Rewards, playerStateVariable.CurrentTarget.transform.position);   
                ship.MoveBackwardsAPath(pathsToIslands.IslandSplines[playerStateVariable.CurrentTarget]);
                playerStateVariable.CurrentTarget.BeatStage();
                waitingForReturnToShip = false;
            }
            
            return;
        }

        if (currentBattle == null || currentBattle.BattleState != Battle.State.InProgress)
        {
            return;
        }

        if (currentBattle.ProgressBattle() != Battle.State.InProgress)
        {
            if (currentBattle.BattleState == Battle.State.AttackerWon)
            {
                waitingForReturnToShip = true;
                return;
            }

            if (currentBattle.BattleState == Battle.State.DefenderWon)
            {
                playerLostEvent.Raise();
            }
        }
    }

    private void onShipArrival()
    {
        if (ship.IsAtEnd())
        {
            currentBattle = BattleCreator.CreateIslandAttackBattle(army, playerStateVariable.CurrentTarget, playerTeam, enemyTeam);
            return;
        }

        if (ship.IsAtBeggining())
        {
            playerStateVariable.ChangeCombatState(PlayerCombatState.Preparation);
            playerStateVariable.ChangeTarget(null); 
            targetCanceledEvent.Raise();
        }
    }

    private void removeSpellCost(Skill _skillCast)
    {
        playerResources.Value -= _skillCast.GoldCost*Resources.OneGold;
    }

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

    public void OnEmbarkPressed()
    {
        if (playerStateVariable.CurrentTarget == null || playerStateVariable.CombatState == PlayerCombatState.Combat)
        {
            return;
        }

        playerStateVariable.ChangeCombatState(PlayerCombatState.Combat);
        ship.MoveForwardAPath(pathsToIslands.IslandSplines[playerStateVariable.CurrentTarget]);
        onEmbarkEvent.Raise();
    }

    public void OnTargetChoice(Island _target)
    {
        if (playerStateVariable.CurrentTarget != null)
        {
            return;
        }

        playerStateVariable.ChangeTarget(_target);
        targetChosenEvent.Raise(playerStateVariable.CurrentTarget);
    }

    public void OnTargetCancel()
    {
        if (playerStateVariable.CurrentTarget == null)
        {
            return;
        }

        playerStateVariable.ChangeTarget(null);
        targetCanceledEvent.Raise();
    }
}
