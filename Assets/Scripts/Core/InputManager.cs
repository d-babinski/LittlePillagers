using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class InputManager : ScriptableObject
{
    public Vector2 MouseWorldPosition => Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
    
    private InputActions availableActions = null;
    
    void OnEnable()
    {
        if (availableActions == null)
        {
            availableActions = new InputActions();
        }
        
        availableActions.Enable();
    }
    
    void OnDisable()
    {
        availableActions.Disable();
    }
    
    public bool PausePressed()
    {
        return availableActions.Player.Pause.triggered;
    }

    public bool SelectPressed()
    {
        return availableActions.Player.Select.triggered;
    }

    public bool ConfirmSkillshotPressed()
    {
        if (availableActions.Player.Select.triggered)
        {
            return EventSystem.current.IsPointerOverGameObject() == false;
        }
        
        return availableActions.Player.Select.triggered;
    }
    
    public bool CancelSkillPressed()
    {
        return availableActions.Player.CancelSkill.triggered;
    }

    public bool FirstSkillPressed()
    {
        return availableActions.Player.FirstSkill.triggered;
    }

    public bool SecondSkillPressed()
    {
        return availableActions.Player.SecondSkill.triggered;
    }

    public bool ThirdSkillPressed()
    {
        return availableActions.Player.ThirdSkill.triggered;
    }
}
