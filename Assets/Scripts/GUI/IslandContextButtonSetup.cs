using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class IslandContextButtonSetup : ScriptableObject
{
    [System.Serializable]
    public struct IslandContextAction
    {
        public enum Action
        {
            Attack = 0,
            CancelAttack = 1,
            Zoom = 2,
            Unzoom = 3,
        }

        public Action ActionId;
        public Sprite ButtonSprite;
        public Vector2 LocalPosition;
    }
    
    public IslandContextAction[] IslandContextActions = null;
    public IslandContextButton ContextButtonPrefab = null;
    
    public IslandContextButton[] CreateContextButtonsForIsland(Island _island)
    {
        IslandContextButton[] _createdButtons = new IslandContextButton[IslandContextActions.Length];
        
        for(int i = 0; i < IslandContextActions.Length; i++)
        {
            IslandContextButton _spawnedButton = Instantiate(ContextButtonPrefab);
            _spawnedButton.ActionId = (int)IslandContextActions[i].ActionId;
            _spawnedButton.GetComponent<SpriteRenderer>().sprite = IslandContextActions[i].ButtonSprite;
            _spawnedButton.transform.position = (Vector2)_island.transform.position + IslandContextActions[i].LocalPosition;
            _spawnedButton.SetContext(_island);

            _createdButtons[i] = _spawnedButton;
        }

        return _createdButtons;
    }
}
