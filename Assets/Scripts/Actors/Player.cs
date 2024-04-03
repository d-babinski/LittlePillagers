using CHARK.ScriptableEvents.Events;
using DefaultNamespace;
using ScriptableEvents.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private IslandScriptableEvent targetChosenEvent = null;
    [SerializeField] private SimpleScriptableEvent targetCanceledEvent = null;
    [SerializeField] private SimpleScriptableEvent onEmbarkEvent = null;
    [SerializeField] private SimpleScriptableEvent playerLostEvent = null;
    [SerializeField] private SimpleScriptableEvent stageBeatenEvent = null;

    [SerializeField] private InputManager input = null;

    [SerializeField] private PlayerSkillCaster playerSkillCaster = null;
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private ResourcesVariable playerResources = null;
    [SerializeField] private Army army = null;
    [SerializeField] private IslandPaths pathsToIslands = null;
    [SerializeField] private PlayerShip ship = null;
    [SerializeField] private ClickableResourceSpawner resourceSpawner = null;

    [SerializeField] private Team playerTeam = null;

    private PlayerIslandAttackSequence currentMission = null;


    private void OnEnable()
    {
        playerSkillCaster.OnSkillCastEvent += removeSpellCost;
    }

    private void OnDisable()
    {
        playerSkillCaster.OnSkillCastEvent -= removeSpellCost;
    }

    private void Update()
    {
        if (playerSkillCaster.IsAiming == true)
        {
            if (input.ConfirmSkillshotPressed() == true)
            {
                playerSkillCaster.ConfirmSkillshot(input.MouseWorldPosition);
            }

            if (input.CancelSkillPressed() == true)
            {
                playerSkillCaster.CancelCast();
            }

        }

        if (input.FirstSkillPressed())
        {
            UseSkill(playerSkillCaster.PlayerSkills[0]);
        }

        if (input.SecondSkillPressed())
        {
            UseSkill(playerSkillCaster.PlayerSkills[1]);
        }

        if (input.ThirdSkillPressed())
        {
            UseSkill(playerSkillCaster.PlayerSkills[2]);
        }
        
        if (currentMission != null)
        {
            PlayerIslandAttackSequence.SequenceState _currentMission = currentMission.UpdateSequence();

            if (_currentMission == PlayerIslandAttackSequence.SequenceState.Failure)
            {
                playerLostEvent.Raise();
                currentMission = null;
                return;
            }

            if (_currentMission == PlayerIslandAttackSequence.SequenceState.Success)
            {
                completeMission();
                stageBeatenEvent.Raise();
                return;
            }

        }
    }

    public void UseSkill(SkillVariable _playerSkill)
    {
        if (_playerSkill.Value.IsUsableDuringPhase(playerStateVariable.CombatState) == false)
        {
            return;
        }

        if (_playerSkill.Value.GoldCost > playerResources.Value.Gold)
        {
            return;
        }

        playerSkillCaster.CastSkill(_playerSkill.Value);
    }

    private void startMission()
    {
        currentMission = new PlayerIslandAttackSequence(playerStateVariable.CurrentTarget, ship, army, pathsToIslands, playerTeam);
        playerStateVariable.ChangeCombatState(PlayerCombatState.Combat);
    }

    private void completeMission()
    {
        resourceSpawner.SpawnResources(playerStateVariable.CurrentTarget.GetCurrentStage().Rewards, playerStateVariable.CurrentTarget.transform.position);
        playerStateVariable.CurrentTarget.BeatStage();
        currentMission = null;
        playerStateVariable.ChangeCombatState(PlayerCombatState.Preparation);
        playerStateVariable.ChangeTarget(null);
        targetCanceledEvent.Raise();
    }

    private void removeSpellCost(Skill _skillCast)
    {
        playerResources.Value -= _skillCast.GoldCost*Resources.OneGold;
    }

    public void AddResources(Resources _res)
    {
        playerResources.Value += _res;
    }

    public void BuyUnits(UnitType _type)
    {
        Resources _unitCost = _type.BaseCost;

        if (playerResources.Value < _unitCost)
        {
            return;
        }

        army.AddUnits(_type);
        playerResources.Value -= _unitCost;
    }

    public void OnEmbarkPressed()
    {
        if (playerStateVariable.CurrentTarget == null || playerStateVariable.CombatState == PlayerCombatState.Combat)
        {
            return;
        }

        startMission();
        onEmbarkEvent.Raise();
    }

    public void OnTargetChoice(Island _target)
    {
        if (playerStateVariable.CurrentTarget != null || _target.AreAllStagesBeaten() == true)
        {
            return;
        }

        playerStateVariable.ChangeTarget(_target);
        targetChosenEvent.Raise(playerStateVariable.CurrentTarget);
    }

    public void OnTargetCancel()
    {
        if (playerStateVariable.CurrentTarget == null)
        {
            return;
        }

        playerStateVariable.ChangeTarget(null);
        targetCanceledEvent.Raise();
    }
}
