using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleUpdater : MonoBehaviour
{
    [SerializeField] private TimeCycler timeCycler = null;
    [SerializeField] private Player player = null;
    [SerializeField] private IsleManager isleUpdater = null;

    private void Start()
    {
        timeCycler.OnNewCycle += UpdateOnNewCycle;
    }
    
    private void UpdateOnNewCycle()
    {
        player.UpdateCycle();
        isleUpdater.UpdateCycle();
    }

}
