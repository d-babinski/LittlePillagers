using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyRowUI : MonoBehaviour
{
    [SerializeField] private ResourceBlockUI resourceBlockUI = null;
    [SerializeField] private ResourceBlockColoring blockColoring = null;

    public void SetValues(Resources _resources)
    {
        resourceBlockUI.SetResources(_resources);
        blockColoring.SetColoringBasedOnResources(_resources);
    }
    
}
