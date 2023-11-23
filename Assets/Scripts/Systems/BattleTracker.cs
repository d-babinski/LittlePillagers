using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BattleTracker : MonoBehaviour
{
    public UnityEvent PlayerWonActions = null;
    public UnityEvent PlayerLostActions = null;

    [SerializeField] private UnitRuntimeSet playerUnits = null;
    [SerializeField] private UnitRuntimeSet aiUnits = null;

    public bool TrackBattle = false;

    public void TrackCurrentBattle()
    {
        TrackBattle = true;
    }
    
    public void Update()
    {
        if (TrackBattle == false)
        {
            return;
        }

        if (playerUnits.Items.Count <= 0)
        {
            PlayerLostActions?.Invoke();
            TrackBattle = false;
            return;
        }

        if (aiUnits.Items.Count <= 0)
        {
            PlayerWonActions?.Invoke();
            TrackBattle = false;
            return;
        }
    }
}