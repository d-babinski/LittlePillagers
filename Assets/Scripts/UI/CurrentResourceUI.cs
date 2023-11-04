using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentResourceUI : MonoBehaviour
{
    [SerializeField] private ResourceBlockUI resourcePanel = null;

    public void SetCurrentResources(Resources _res)
    {
        resourcePanel.SetResources(_res);
    }
}
