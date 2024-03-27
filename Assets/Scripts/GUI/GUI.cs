using CHARK.ScriptableEvents.Events;
using ScriptableEvents.Events;
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

    public IslandNameplateSetup IslandNameplateSetup = null;
    [FormerlySerializedAs("ContextButtons")] public IslandContextButtonSetup ContextButtonSetup = null;
    public IslandRuntimeSet IslandRuntimeSet = null;
    [SerializeField] private IslandStageIcon stageIconPrefab = null;
    
    private Dictionary<Island, IslandContextButton[]> islandContextButtons = new();

    public void SpawnGUI()
    {
        SpawnIslandContextButtons(IslandRuntimeSet.Items);
        SubscribeToContextButtonEvents();
        SpawnIslandNameplates(IslandRuntimeSet.Items);
        SpawnStageIcons(IslandRuntimeSet.Items);
    }
    
    private void SpawnIslandNameplates(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
              IslandNameplateSetup.CreateNameplateForIsland(_island);
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
    
    //TODO : Simplify action enum path
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
                Instantiate(stageIconPrefab).Initialize(_island, i);
            }
        });
    }

    public void SpawnIslandContextButtons(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
            IslandContextButton[] _islandButtons = ContextButtonSetup.CreateContextButtonsForIsland(_island);

            islandContextButtons[_island] = _islandButtons;

            for (int i = 0; i < _islandButtons.Length; i++)
            {
                if (_islandButtons[i].Action == IslandContextAction.Zoom || _islandButtons[i].Action == IslandContextAction.Unzoom)
                {
                    AddZoomStateDependencyToButton(_islandButtons[i], zoomStateVariable);
                    continue;
                }

                if (_islandButtons[i].Action == IslandContextAction.None)
                {
                    continue;
                }
                
                AddPlayerStateDependancyToButton(_islandButtons[i], playerStateVariable);
            }
        });
    }
    
    private void AddZoomStateDependencyToButton(IslandContextButton _button, ZoomStateVariable _zoomStateVariable)
    {
        ZoomStateDependency _zoomStateDependency = _button.gameObject.AddComponent<ZoomStateDependency>();
        bool _requiresZoomIn = _button.Action == IslandContextAction.Unzoom ? true : false;
        
        _zoomStateDependency.SetStateDependancy(_button.gameObject, _zoomStateVariable, _requiresZoomIn);
        
    }

    public void AddPlayerStateDependancyToButton(IslandContextButton _button, PlayerStateVariable _playerState)
    {
        PlayerStateDependency _playerStateDependency = _button.gameObject.AddComponent<PlayerStateDependency>();
        PlayerCombatState _combatStateDependancy = PlayerCombatState.Preparation;
        PlayerTargetState _targetStateDependancy = _button.Action == IslandContextAction.Attack ? PlayerTargetState.NotChosen : PlayerTargetState.Chosen;
        
        _playerStateDependency.SetStateDependancy(_button.gameObject, _playerState,_combatStateDependancy,_targetStateDependancy);

        if (_button.Action == IslandContextAction.CancelAttack)
        {
            _playerStateDependency.SetTargetRequirement(true,_button.GetContext());
        }
    }
}
