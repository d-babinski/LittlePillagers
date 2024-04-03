using CHARK.ScriptableEvents.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkillUI : MonoBehaviour
{
    [SerializeField] private SimpleScriptableEvent eventToCallOnPress = null;
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private SkillVariable assignedSkill = null;
    [SerializeField] private ResourcesVariable playerResources = null;

    private Button button = null;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        bool _canAfford = playerResources.Value.Gold >= assignedSkill.Value.GoldCost;
        bool _shouldBeInteractable = _canAfford && assignedSkill.Value.IsUsableDuringPhase(playerStateVariable.CombatState);

        if (_shouldBeInteractable != button.interactable)
        {
            button.interactable = _shouldBeInteractable;
        }
    }

    public void UseSkill()
    {
        if (button.interactable == false)
        {
            return;
        }
        
        eventToCallOnPress.Raise();
    }
}
