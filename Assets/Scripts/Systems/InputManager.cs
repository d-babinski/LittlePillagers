using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public InputActions AvailableActions => availableActions;
    
    private InputActions availableActions = null;

    public UnityEvent OnCancelSkillClicked = null;
    public UnityEvent OnPauseClicked = null;
    public UnityEvent OnSelectClicked = null;
    public UnityEvent OnFirstSkillPressed = null;
    public UnityEvent OnSecondSkillPressed = null;
    public UnityEvent OnThirdSkillPressed = null;

    private void Awake()
    {
        availableActions = new InputActions();
    }

    public bool PausePressed()
    {
        return availableActions.Player.Pause.triggered;
    }

    public bool SelectPressed()
    {
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
    
    private void Update()
    {
        if (availableActions.Player.Pause.triggered)
        {
            OnPauseClicked?.Invoke();
        }

        if (availableActions.Player.Select.triggered)
        {
            OnSelectClicked?.Invoke();
        }
        
        if (availableActions.Player.CancelSkill.triggered)
        {
            OnCancelSkillClicked?.Invoke();
        }

        if (availableActions.Player.FirstSkill.triggered)
        {
            OnFirstSkillPressed?.Invoke();
        }
        
        if (availableActions.Player.SecondSkill.triggered)
        {
            OnSecondSkillPressed?.Invoke();
        }
        
        if (availableActions.Player.ThirdSkill.triggered)
        {
            OnThirdSkillPressed?.Invoke();
        }
    }

    void OnEnable()
    {
        availableActions.Enable();
    }
    void OnDisable()
    {
        availableActions.Disable();
    }
}
