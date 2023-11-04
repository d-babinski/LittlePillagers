using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battlefield : MonoBehaviour
{
    public struct FactionSoldier
    {
        public int Team;
        public Soldier Soldier;

        public FactionSoldier(int _team, Soldier _soldier)
        {
            Team = _team;
            Soldier = _soldier;
        }
    }

    public struct Duel
    {
        public FactionSoldier LeftSide;
        public FactionSoldier RightSide;

        public Duel(FactionSoldier _leftSide, FactionSoldier _rightSide)
        {
            LeftSide = _leftSide;
            RightSide = _rightSide;
        }
    }

    private const int PLAYER_TEAM = 0;
    private const int AI_TEAM = 1;

    public event Action<List<Soldier>> OnBattlefieldConcluded = null;
    
    private List<FactionSoldier> soldiersTakingPart = new();
    private List<Duel> ongoingDuels = new();
    public bool IsResolved => isResolved;

    private bool isResolved = false;

    private void Update()
    {
        if (isResolved == false)
        {
            while (createNewDuelIfPossible())
            {
                continue;
            }

            progressDuels();

            if (checkForWin())
            {
                isResolved = true;
                sendBackWinners();
                OnBattlefieldConcluded?.Invoke(getAliveSoldiers());
            }

            return;
        }
    }
    
    private List<Soldier> getAliveSoldiers()
    {
        List<Soldier> _soldiersAlive = new();
        
        soldiersTakingPart.ForEach(_soldier =>
        {
            _soldiersAlive.Add(_soldier.Soldier);
        });

        return _soldiersAlive;
    }

    private void sendBackWinners()
    { 
        soldiersTakingPart.ForEach(_soldier =>
        {
            _soldier.Soldier.ReturnToRallyPoint();
        });
    }

    private bool checkForWin()
    {
        if (soldiersTakingPart.Count <= 0)
        {
            return true;
        }

        bool _playerAlive = isThereAnySoldierOfTeam(PLAYER_TEAM);
        bool _aiAlive = isThereAnySoldierOfTeam(AI_TEAM);

        if (_playerAlive && _aiAlive)
        {
            return false;
        }

        return true;
    }

    private bool isThereAnySoldierOfTeam(int _team)
    {
        for (int i = 0; i < soldiersTakingPart.Count; i++)
        {
            if (soldiersTakingPart[i].Team == _team)
            {
                return true;
            }
        }

        return false;
    }

    private bool createNewDuelIfPossible()
    {
        FactionSoldier _leftSide = getFirstFreeSoldierOfTeam(PLAYER_TEAM);
        FactionSoldier _rightSide = getFirstFreeSoldierOfTeam(AI_TEAM);

        if (_leftSide.Soldier == false || _rightSide.Soldier == false)
        {
            return false;
        }

        ongoingDuels.Add(new Duel(_leftSide, _rightSide));

        Vector3 _leftSoldierPos = _leftSide.Soldier.transform.position;
        Vector3 _rightSoldierPos = _rightSide.Soldier.transform.position;
        Vector3 _vectorFromLeftToRight = (_rightSoldierPos - _leftSoldierPos);
        
        Vector3 _meetPosition = _leftSoldierPos + _vectorFromLeftToRight/2f;
        Vector3 _leftSideOffset =  -_vectorFromLeftToRight.normalized*0.02f;
        Vector3 _rightSideOffset = _vectorFromLeftToRight.normalized*0.02f;

        _leftSide.Soldier.MoveTo(_meetPosition + _leftSideOffset);
        _rightSide.Soldier.MoveTo(_meetPosition + _rightSideOffset);

        return true;
    }

    private FactionSoldier getFirstFreeSoldierOfTeam(int _team)
    {
        for (int i = 0; i < soldiersTakingPart.Count; i++)
        {
            if (soldiersTakingPart[i].Team == _team && hasDuel(soldiersTakingPart[i]) == false)
            {
                return soldiersTakingPart[i];
            }
        }

        return new FactionSoldier();
    }

    private bool hasDuel(FactionSoldier _factionSoldier)
    {
        for (int i = 0; i < ongoingDuels.Count; i++)
        {
            if (ongoingDuels[i].LeftSide.Soldier == _factionSoldier.Soldier || ongoingDuels[i].RightSide.Soldier == _factionSoldier.Soldier)
            {
                return true;
            }
        }

        return false;
    }

    private void progressDuels()
    {
        ongoingDuels.ForEach(_duel =>
        {
            if (_duel.LeftSide.Soldier.IsIdle)
            {
                _duel.LeftSide.Soldier.MakeAttack();
                _duel.RightSide.Soldier.MakeAttack();
                DOVirtual.DelayedCall(Random.Range(1f, 3f), () => ResolveDuel(_duel));
            }
        });
    }

    public void AddPlayerUnit(Soldier _soldier)
    {
        soldiersTakingPart.Add(new FactionSoldier(PLAYER_TEAM, _soldier));
    }

    public void AddAIUnit(Soldier _soldier)
    {
        soldiersTakingPart.Add(new FactionSoldier(AI_TEAM, _soldier));
    }

    public void ResolveDuel(Duel _duel)
    {
        _duel.LeftSide.Soldier.TakeDamage(_duel.RightSide.Soldier.Attack);
        _duel.RightSide.Soldier.TakeDamage(_duel.LeftSide.Soldier.Attack);

        if (_duel.RightSide.Soldier.AttackLeft <= 0)
        {
            _duel.RightSide.Soldier.Die();
            removeFromBattlefield(_duel.RightSide);
        }
        else
        {
            _duel.RightSide.Soldier.Wait();
        }

        if (_duel.LeftSide.Soldier.AttackLeft <= 0)
        {
            _duel.LeftSide.Soldier.Die();
            removeFromBattlefield(_duel.LeftSide);
        }
        else
        {
            _duel.LeftSide.Soldier.Wait();
        }

        ongoingDuels.Remove(_duel);
    }

    private void removeFromBattlefield(FactionSoldier _soldier)
    {
        soldiersTakingPart.Remove(_soldier);
    }

    //take care of sending soldiers to duel, destroy, sending back to origin, death
}
