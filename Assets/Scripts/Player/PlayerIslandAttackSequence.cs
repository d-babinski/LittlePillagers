using DefaultNamespace;
using System;

public class PlayerIslandAttackSequence
{
    public enum SequenceState
    {
        Init = -1,
        TravelingTo = 0,
        Combat = 1,
        WaitingForSoldiers = 2,
        TravelingBack = 3,
        Success = 4,
        Failure = 5,
    }

    public SequenceState CurrentState => currentState;

    private SequenceState currentState = SequenceState.Init;

    private Island target = null;
    private PlayerShip ship = null;
    private Army playerArmy = null;
    private Battle currentBattle = null;
    private IslandPaths islandPaths = null;
    private Team playerTeam = null;

    public PlayerIslandAttackSequence(Island _target, PlayerShip _ship, Army _playerArmy, IslandPaths _paths, Team _playerTeam)
    {
        target = _target;
        ship = _ship;
        playerArmy = _playerArmy;
        islandPaths = _paths;
        playerTeam = _playerTeam;
        
        ChangeState(SequenceState.TravelingTo);
    }

    public void ChangeState(SequenceState _newState)
    {
        switch (_newState)
        {
            case SequenceState.TravelingTo:
                ship.MoveForwardAPath(islandPaths.IslandSplines[target]);
                break;
            case SequenceState.Combat:
                currentBattle = BattleCreator.CreateIslandAttackBattle(playerArmy, target, playerTeam, target.IslandTeam);
                break;
            case SequenceState.WaitingForSoldiers:
                break;
            case SequenceState.TravelingBack:
                ship.MoveBackwardsAPath(islandPaths.IslandSplines[target]);
                break;
            case SequenceState.Success:
                break;
            case SequenceState.Failure:
                break;
        }

        currentState = _newState;
    }

    public SequenceState UpdateSequence()
    {
        switch (CurrentState)
        {
            case SequenceState.TravelingTo:
                if (ship.IsAtEnd())
                {
                    ChangeState(SequenceState.Combat);
                }
                break;
            case SequenceState.Combat:
                if (currentBattle.ProgressBattle() != Battle.State.InProgress)
                {
                    if (currentBattle.BattleState == Battle.State.AttackerWon)
                    {
                        ChangeState(SequenceState.WaitingForSoldiers);
                    }

                    if (currentBattle.BattleState == Battle.State.DefenderWon)
                    {
                        ChangeState(SequenceState.Failure);
                    }
                }
                break;
            case SequenceState.WaitingForSoldiers:
                if (MoveToShipAI.MoveAliveUnitsToPointAndDestroy(currentBattle.AttackerUnits, ship.transform.position) == MoveToShipAI.State.AllUnitsMoved)
                {
                    ChangeState(SequenceState.TravelingBack);
                }
                break;
            case SequenceState.TravelingBack:
                if (ship.IsAtBeggining())
                {
                    ChangeState(SequenceState.Success);
                }
                break;
            case SequenceState.Success:
                break;
            case SequenceState.Failure:
                break;
        }

        return CurrentState;
    }
}
