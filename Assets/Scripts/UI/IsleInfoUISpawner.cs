using System;
using UnityEngine;

public class VisibilityTweenPool : MonoBehaviour
{
    [SerializeField] private VisibilityTween[] isleUIPool = Array.Empty<VisibilityTween>();

    private int lastShownId = 0;

    public void Hide()
    {
        isleUIPool[lastShownId].Hide();
    }

    public void Show()
    {
        lastShownId++;

        if (lastShownId > isleUIPool.Length - 1)
        {
            lastShownId = 0;
        }

        isleUIPool[lastShownId].Show();
    }
}
