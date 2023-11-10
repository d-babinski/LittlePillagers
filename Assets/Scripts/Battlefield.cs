using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Battlefield : MonoBehaviour
{
    private List<Soldier> idleAttackers = new();
    private List<Soldier> idleDefenders = new();

    public struct Duel
    {
        public Soldier Attacker;
        public Soldier Defender;

        public Duel(Soldier _attacker, Soldier _defender)
        {
            Attacker = _attacker;
            Defender = _defender;
        }
    }

    private List<Duel> ongoingDuels = new();

    private void Update()
    {
        if (ongoingDuels.Count > 0)
        {
            progressDuels();
        }

        if (idleAttackers.Count <= 0 || idleDefenders.Count <= 0)
        {
            return;
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
            if (_duel.Attacker.IsIdle)
            {
                _duel.Attacker.MakeAttack();
                _duel.Defender.MakeAttack();
                DOVirtual.DelayedCall(Random.Range(1f, 3f), () => ResolveDuel(_duel));
            }
        });
    }

    public void AddAttacker(Soldier _soldier)
    {
        idleAttackers.Add(_soldier);
    }

    public void AddDefenders(Soldier _soldier)
    {
        idleDefenders.Add(_soldier);
    }

    public void ResolveDuel(Duel _duel)
    {
        _duel.Attacker.TakeDamage(_duel.Defender.Attack.Value);
        _duel.Defender.TakeDamage(_duel.Attacker.Attack.Value);

        if (_duel.Defender.AttackLeft <= 0)
        {
            _duel.Defender.Die();
            removeFromBattlefield(_duel.Defender);
        }
        else
        {
            _duel.Defender.Wait();
        }

        if (_duel.Attacker.AttackLeft <= 0)
        {
            _duel.Attacker.Die();
            removeFromBattlefield(_duel.Attacker);
        }
        else
        {
            _duel.Attacker.Wait();
        }

        ongoingDuels.Remove(_duel);
    }

    private void removeFromBattlefield(Soldier _soldier)
    {
        idleAttackers.Remove(_soldier);
    }
}

