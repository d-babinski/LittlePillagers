using CHARK.ScriptableEvents.Events;
using ScriptableEvents.Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GUI : MonoBehaviour
{
    [Header("GUI Events")]
    [FormerlySerializedAs("ChooseAsTargetButtonClicked")][SerializeField] private IslandScriptableEvent chooseAsTargetButtonClicked = null;
    [FormerlySerializedAs("CancelTargetButtonClicked")][SerializeField] private SimpleScriptableEvent cancelTargetButtonClicked = null;
    [FormerlySerializedAs("ZoomToIslandButtonClicked")][SerializeField] private IslandScriptableEvent zoomToIslandButtonClicked = null;
    [FormerlySerializedAs("UnzoomButtonClicked")][SerializeField] private SimpleScriptableEvent unzoomButtonClicked = null;
    
    [Header("Core References")]
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private ZoomStateVariable zoomStateVariable = null;
    [SerializeField] private Canvas guiCanvas = null;
    
    [Header("Island GUI Related")]
    [FormerlySerializedAs("IslandNameplateSetup")][SerializeField] private IslandNameplateSetup islandNameplateSetup = null;
    [FormerlySerializedAs("ContextButtonSetup")][FormerlySerializedAs("ContextButtons")][SerializeField] private IslandContextButtonSetup contextButtonSetup = null;
    [FormerlySerializedAs("IslandRuntimeSet")][SerializeField] private IslandRuntimeSet islandRuntimeSet = null;
    [SerializeField] private IslandStageIcon stageIconPrefab = null;
    
    private Dictionary<Island, IslandContextButton[]> islandContextButtons = new();

    public void SpawnGUI()
    {
        guiCanvas.worldCamera = Camera.main;
        spawnIslandContextButtons(islandRuntimeSet.Items);
        subscribeToContextButtonEvents();
        spawnIslandNameplates(islandRuntimeSet.Items);
        spawnStageIcons(islandRuntimeSet.Items);
    }
    
    private void spawnIslandNameplates(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
              islandNameplateSetup.CreateNameplateForIsland(_island, transform);
        });
    }

    private void subscribeToContextButtonEvents()
    {
        islandRuntimeSet.Items.ForEach(_island =>
        {
            for (int i = 0; i < islandContextButtons[_island].Length; i++)
            {
                islandContextButtons[_island][i].OnActionClicked += handleContextButtonClicked;
            }
        });
    }
    
    private void handleContextButtonClicked(IslandContextAction _contextAction, Island _context)
    {
        switch (_contextAction)
        {
            case IslandContextAction.Attack:
                chooseAsTargetButtonClicked.Raise(_context);
                break;
            case IslandContextAction.CancelAttack:
                cancelTargetButtonClicked.Raise();
                break;
            case IslandContextAction.Zoom:
                zoomToIslandButtonClicked.Raise(_context);
                break;
            case IslandContextAction.Unzoom:
                unzoomButtonClicked.Raise();
                break;
        }
    }

    private void spawnStageIcons(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
            int _iconsToSpawn = _island.TotalStageCount;

            for (int i = 0; i < _iconsToSpawn; i++)
            {
                Instantiate(stageIconPrefab, transform).Initialize(_island, i);
            }
        });
    }

    private void spawnIslandContextButtons(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
            IslandContextButton[] _islandButtons = contextButtonSetup.CreateContextButtonsForIsland(_island, transform);

            islandContextButtons[_island] = _islandButtons;

            for (int i = 0; i < _islandButtons.Length; i++)
            {
                PredicateBasedActions _predicateActions = _islandButtons[i].gameObject.AddComponent<PredicateBasedActions>();
                IslandContextButton _button = _islandButtons[i];

                _predicateActions.OnAllPredicatesFulfilledEvent.AddListener(() => _button.gameObject.SetActive(true));
                _predicateActions.OnAnyPredicateFailedEvent.AddListener( () => _button.gameObject.SetActive(false));
                
                
                switch (_button.Action)
                {
                    case IslandContextAction.Attack:
                        _predicateActions.AddPredicate(new PlayerCombatStatePredicate(playerStateVariable, PlayerCombatState.Preparation));
                        _predicateActions.AddPredicate(new PlayerTargetStatePredicate(playerStateVariable, PlayerTargetState.NotChosen));
                        _predicateActions.AddPredicate(new IslandBeatenPredicate(_button.GetContext(), false));
                        break;
                    case IslandContextAction.CancelAttack:
                        _predicateActions.AddPredicate(new PlayerCombatStatePredicate(playerStateVariable, PlayerCombatState.Preparation));
                        _predicateActions.AddPredicate(new PlayerTargetStatePredicate(playerStateVariable, PlayerTargetState.Chosen));
                        _predicateActions.AddPredicate(new IslandPredicate(playerStateVariable, _button.GetContext()));
                        break;
                    case IslandContextAction.Zoom:
                        _predicateActions.AddPredicate(new ZoomStatePredicate(zoomStateVariable, false));
                        break;
                    case IslandContextAction.Unzoom:
                        _predicateActions.AddPredicate(new ZoomStatePredicate(zoomStateVariable, true));
                        break;
                }
            }
        });
    }
}
