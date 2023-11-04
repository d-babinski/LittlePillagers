using System;
using UnityEngine;

public class MissionTypeWindowUI : MonoBehaviour
{
    public event Action OnSendButtonPressed = null;

    public MissionType ChosenMissionType => (MissionType)missionTypeToggleGroup.CurrentChosenOption;
    
    [SerializeField] private ToggleGroupUI missionTypeToggleGroup = null;
    [SerializeField] private ImageButton sendOnMissionButton = null;
    [SerializeField] private SidePanel sidePanelController = null;

    private void Awake()
    {
        sendOnMissionButton.OnButtonClicked += sendButtonPressed;
    }

    private void OnDestroy()
    {
        sendOnMissionButton.OnButtonClicked -= sendButtonPressed;
    }

    public void OpenWindow()
    {
        sidePanelController.Open();
    }

    public void CloseWindow()
    {
        sidePanelController.Close();
    }
    
    public void SelectOption(int _id)
    {
        missionTypeToggleGroup.Select(_id);
    }
    
    private void sendButtonPressed()
    {
        OnSendButtonPressed?.Invoke();
    }

    public void SetClosed()
    {
        sidePanelController.CloseInstantly();
    }
}
