using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleZoomCamera : MonoBehaviour
{
    public const int HIGH_PRIORITY = 20;
    public const int LOW_PRIORITY = 0;
    
    [SerializeField] private CinemachineVirtualCamera uiCam = null;
    
    public void ShowForIsle(Isle _isle)
    {
        uiCam.transform.position = new Vector3(_isle.transform.position.x, _isle.transform.position.y, uiCam.transform.position.z);
        uiCam.Priority = HIGH_PRIORITY;
    }

    public void Hide()
    {
        uiCam.Priority = LOW_PRIORITY;
    }
}
 