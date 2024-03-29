using CHARK.ScriptableEvents.Events;
using ScriptableEvents.Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GUI : MonoBehaviour
{
    [SerializeField] private IslandScriptableEvent ChooseAsTargetButtonClicked = null;
    [SerializeField] private SimpleScriptableEvent CancelTargetButtonClicked = null;
    [SerializeField] private IslandScriptableEvent ZoomToIslandButtonClicked = null;
    [SerializeField] private SimpleScriptableEvent UnzoomButtonClicked = null;
    [SerializeField] private PlayerStateVariable playerStateVariable = null;
    [SerializeField] private ZoomStateVariable zoomStateVariable = null;
    [SerializeField] private Canvas guiCanvas = null;
    
    public IslandNameplateSetup IslandNameplateSetup = null;
    [FormerlySerializedAs("ContextButtons")] public IslandContextButtonSetup ContextButtonSetup = null;
    public IslandRuntimeSet IslandRuntimeSet = null;
    [SerializeField] private IslandStageIcon stageIconPrefab = null;
    
    private Dictionary<Island, IslandContextButton[]> islandContextButtons = new();

    public void SpawnGUI()
    {
        guiCanvas.worldCamera = Camera.main;
        SpawnIslandContextButtons(IslandRuntimeSet.Items);
        SubscribeToContextButtonEvents();
        SpawnIslandNameplates(IslandRuntimeSet.Items);
        SpawnStageIcons(IslandRuntimeSet.Items);
    }
    
    private void SpawnIslandNameplates(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
              IslandNameplateSetup.CreateNameplateForIsland(_island, transform);
        });
    }

    public void SubscribeToContextButtonEvents()
    {
        IslandRuntimeSet.Items.ForEach(_island =>
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
                ChooseAsTargetButtonClicked.Raise(_context);
                break;
            case IslandContextAction.CancelAttack:
                CancelTargetButtonClicked.Raise();
                break;
            case IslandContextAction.Zoom:
                ZoomToIslandButtonClicked.Raise(_context);
                break;
            case IslandContextAction.Unzoom:
                UnzoomButtonClicked.Raise();
                break;
        }
    }

    public void SpawnStageIcons(List<Island> _islands)
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

    public void SpawnIslandContextButtons(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
            IslandContextButton[] _islandButtons = ContextButtonSetup.CreateContextButtonsForIsland(_island, transform);

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
