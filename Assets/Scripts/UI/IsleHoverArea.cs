using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsleHoverArea : MonoBehaviour
{
    public Isle TrackedIsle => trackedIsle;
    
    [SerializeField] private Isle trackedIsle = null;
}
