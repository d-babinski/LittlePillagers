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

    public IslandNameplateSetup IslandNameplateSetup = null;
    [FormerlySerializedAs("ContextButtons")] public IslandContextButtonSetup ContextButtonSetup = null;
    public IslandRuntimeSet IslandRuntimeSet = null;

    private Dictionary<Island, IslandContextButton[]> islandContextButtons = new();

    public void SpawnGUI()
    {
        SpawnIslandContextButtons(IslandRuntimeSet.Items);
        SubscribeToContextButtonEvents();
        DisableAllContextButtons();
        EnableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Attack);
        EnableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Zoom);
        SpawnIslandNameplates(IslandRuntimeSet.Items);
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
    private void handleContextButtonClicked(int _actionId, Island _context)
    {
        switch (_actionId)
        {
            case (int)IslandContextButtonSetup.IslandContextAction.Action.Attack:
                ChooseAsTargetButtonClicked.Raise(_context);
                break;
            case (int)IslandContextButtonSetup.IslandContextAction.Action.CancelAttack:
                CancelTargetButtonClicked.Raise();
                break;
            case (int)IslandContextButtonSetup.IslandContextAction.Action.Zoom:
                ZoomToIslandButtonClicked.Raise(_context);
                break;
            case (int)IslandContextButtonSetup.IslandContextAction.Action.Unzoom:
                UnzoomButtonClicked.Raise();
                break;
        }
    }

    public void OnIslandChosenAsTarget(Island _context)
    {
        DisableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Attack);
        islandContextButtons[_context][(int)IslandContextButtonSetup.IslandContextAction.Action.CancelAttack].gameObject.SetActive(true);
    }

    public void OnTargetCanceled()
    {
        DisableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.CancelAttack);
        EnableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Attack);
    }

    public void OnIslandZoomed(Island _context)
    {
        DisableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Zoom);
        EnableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Unzoom);
    }

    public void OnIslandUnzoomed()
    {
        DisableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Unzoom);
        EnableAllContextButtonsOfId((int)IslandContextButtonSetup.IslandContextAction.Action.Zoom);
    }
    
    public void SpawnIslandContextButtons(List<Island> _islands)
    {
        _islands.ForEach(_island =>
        {
            islandContextButtons[_island] = ContextButtonSetup.CreateContextButtonsForIsland(_island);
        });
    }

    public void DisableAllContextButtons()
    {
        IslandRuntimeSet.Items.ForEach(_island =>
        {
            for (int i = 0; i < islandContextButtons[_island].Length; i++)
            {
                islandContextButtons[_island][i].gameObject.SetActive(false);
            }
        });    
    }

    public void EnableAllContextButtonsOfId(int _id)
    {
        IslandRuntimeSet.Items.ForEach(_island =>
        {
            islandContextButtons[_island][_id].gameObject.SetActive(true);
        });  
    }
    
    public void DisableAllContextButtonsOfId(int _id)
    {
        IslandRuntimeSet.Items.ForEach(_island =>
        {
            islandContextButtons[_island][_id].gameObject.SetActive(false);
        });  
    }
}
