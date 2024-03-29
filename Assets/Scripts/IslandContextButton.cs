using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class IslandContextButton : MonoBehaviour
{
    public event Action<IslandContextAction, Island> OnActionClicked = null;
    public IslandContextAction Action = IslandContextAction.None;

    [SerializeField] private Island trackedIsland = null;

    private Button trackedButton = null;

    private void Awake()
    {
        trackedButton = GetComponent<Button>();
    }

    public void SetContext(Island _trackedIsland)
    {
        trackedIsland = _trackedIsland;
    }

    public Island GetContext()
    {
        return trackedIsland;
    }

    private void OnEnable()
    {
        trackedButton.onClick.AddListener(sendEvent);
    }

    private void OnDisable()
    {
        trackedButton.onClick.RemoveListener(sendEvent);
    }

    private void sendEvent()
    {
        OnActionClicked?.Invoke(Action, trackedIsland);
    }
}
