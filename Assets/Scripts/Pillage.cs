using System.Collections.Generic;
using UnityEngine;

public class Pillage : MonoBehaviour
{
    public Resources PillagedThisCycle => pillagedThisCycle;
        
    private List<Mission> activeMissions = new();
    private Resources pillagedThisCycle = new Resources();
   
    
    public void UpdateCycle()
    {
        pillagedThisCycle = new Resources();
    }
    
    public void AddPilage(Resources _newPillage)
    {
        pillagedThisCycle += _newPillage;
    }
}
