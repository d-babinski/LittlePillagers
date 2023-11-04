using System;
using UnityEngine;

public class TimeCycler : MonoBehaviour
{
    public event Action OnNewCycle = null;
    public int CurrentCycle => currentCycle;
    
    [SerializeField] private float cycleTime = 1.5f;

    private float currentCycleTime = 0f;
    private int currentCycle = 1;
    private static TimeCycler instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        currentCycleTime += Time.deltaTime;

        if (currentCycleTime >= cycleTime)
        {
            currentCycleTime = 0f;
            currentCycle++;
            OnNewCycle?.Invoke();
        }
    }

    public float GetCurrentCycleProgress()
    {
        return currentCycleTime/cycleTime;
    }
}

