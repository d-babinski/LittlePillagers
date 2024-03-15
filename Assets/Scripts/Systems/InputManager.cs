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
