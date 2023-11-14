using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battlefield : MonoBehaviour
{
    public event Action OnBattleConcluded = null;
    
    private List<Soldier> idleAttackers = new();
    private List<Soldier> idleDefenders = new();
    private List<Duel> ongoingDuels = new();

    private void Update()
    {
        if (idleAttackers.Count <= 0 && idleDefenders.Count <= 0 && ongoingDuels.Count <= 0)
        {
            return;
        }
        
        if (ongoingDuels.Count > 0)
        {
            progressDuels();
            
            if (ongoingDuels.Count <= 0 && (idleAttackers.Count <= 0 || idleDefenders.Count <= 0))
            {
                OnBattleConcluded?.Invoke();
                cleanup();
                return;
            }
        }

        while (createNewDuelIfPossible())
        {
            continue;
        }
    }

    private void cleanup()
    {
        idleAttackers.Clear();
        idleDefenders.Clear();
        ongoingDuels.Clear();
    }

    private bool createNewDuelIfPossible()
    {
        if (idleAttackers.Count <= 0 || idleDefenders.Count <= 0)
        {
            return false;
        }

        ongoingDuels.Add(new Duel(idleAttackers[^1], idleDefenders[^1]));
        idleAttackers.RemoveAt(idleAttackers.Count - 1);
        idleDefenders.RemoveAt(idleDefenders.Count - 1);

        return true;
    }

    private void progressDuels()
    {
        ongoingDuels.ForEach(_duel =>
        {
            if (_duel.HasBegun == false)
            {
                _duel.MeetInTheMiddle();
                return;
            }

            if (_duel.AreSoldiersMoving == true && _duel.HaveReachedPositions() == true)
            {
                _duel.PlayAttacks();
                DOVirtual.DelayedCall(Random.Range(0.75f, 2f), () => ResolveDuel(_duel));
            }
        });
    }

    public void AddAttacker(Soldier _soldier)
    {
        idleAttackers.Add(_soldier);
    }

    public void AddDefender(Soldier _soldier)
    {
        idleDefenders.Add(_soldier);
    }

    public void ResolveDuel(Duel _duel)
    {
        _duel.ResolveDuel();

        if (_duel.Attacker.IsDead == false)
        {
            idleAttackers.Add(_duel.Attacker);
        }

        if (_duel.Defender.IsDead == false)
        {
            idleDefenders.Add(_duel.Defender);
        }

        ongoingDuels.Remove(_duel);
    }
}

