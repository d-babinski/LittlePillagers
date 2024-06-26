﻿using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu()]
public class IslandContextButtonSetup : ScriptableObject
{
    [System.Serializable]
    public struct IslandContextButtonData
    {
        public IslandContextAction IslandContextAction;
        public Sprite ButtonSprite;
        public Vector2 LocalPosition;
    }
    
    public IslandContextButtonData[] IslandContextButtonsData = null;
    public IslandContextButton ContextButtonPrefab = null;
    
    public IslandContextButton[] CreateContextButtonsForIsland(Island _island, Transform _parent)
    {
        IslandContextButton[] _createdButtons = new IslandContextButton[IslandContextButtonsData.Length];
        
        for(int i = 0; i < IslandContextButtonsData.Length; i++)
        {
            IslandContextButton _spawnedButton = Instantiate(ContextButtonPrefab, _parent, true);
            _spawnedButton.Action = IslandContextButtonsData[i].IslandContextAction;
            _spawnedButton.GetComponent<Image>().sprite = IslandContextButtonsData[i].ButtonSprite;
            _spawnedButton.transform.position = (Vector2)_island.transform.position + IslandContextButtonsData[i].LocalPosition;
            _spawnedButton.SetContext(_island);

            _createdButtons[i] = _spawnedButton;
        }

        return _createdButtons;
    }
}
