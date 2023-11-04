using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGroupUI : MonoBehaviour
{
    public int CurrentChosenOption => currentChosenOption;
    
    private ToggleUI[] toggles = null;
    private int currentChosenOption = 0;
    
    private void Awake()
    {
        toggles = GetComponentsInChildren<ToggleUI>();
    }

    private void Start()
    {
        foreach (ToggleUI _uiToggle in toggles)
        {
            _uiToggle.OnToggleStateChanged += onToggleClicked;
        }
        
        toggles[currentChosenOption].SetToggleState(true);
        onToggleClicked(toggles[currentChosenOption]);
    }

    private void OnDestroy()
    {
        foreach (ToggleUI _uiToggle in toggles)
        {
            _uiToggle.OnToggleStateChanged -= onToggleClicked;
        }
    }

    private void onToggleClicked(ToggleUI _toggle)
    {
        selectToggle(_toggle);
    }
    
    private void selectToggle(ToggleUI _toggle)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i] == _toggle)
            {
                currentChosenOption = i;
                continue;
            }

            toggles[i].SetToggleState(false);
        }
    }

    public void Select(int _id)
    {
       selectToggle(toggles[_id]);
    }
}
