using System;
using UnityEngine;

public class UnitManagementWindow : MonoBehaviour
{
    [SerializeField] private Window windowControl = null;
    
    public void Open()
    {
        if (windowControl.IsOpen() == true)
        {
            return;
        }
        
        windowControl.Open();
    }

    public void Close()
    {
        if (windowControl.IsOpen() == false)
        {
            return;
        }
        
        windowControl.Close();
    }

    public void Toggle()
    {
        if (windowControl.IsOpen())
        {
            windowControl.Close();
            return;
        }
        
        windowControl.Open();
    }

    public void CloseWithoutTransition()
    {
        windowControl.CloseInstantly();
    }
}
