using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int CurrentLevel => currentLevel;
    
    [SerializeField] public List<Sprite> spritesTop = new();
    [SerializeField] public List<Sprite> spritesBottom = new();

    [SerializeField] private SpriteRenderer spriteRendererTop = null;
    [SerializeField] private SpriteRenderer spriteRendererBottom = null;

    private int currentLevel = 0; 
    
    private void Awake()
    {
        HideWall();
    }

    public void ShowWall(int _level)
    {
        spriteRendererTop.enabled = true;
        spriteRendererTop.sprite = spritesTop[Mathf.Clamp(_level - 1, 0, spritesTop.Count-1)];
        
        spriteRendererBottom.enabled = true;
        spriteRendererBottom.sprite = spritesBottom[Mathf.Clamp(_level - 1, 0, spritesBottom.Count-1)];

        currentLevel = _level;
    }

    public void HideWall()
    {
        spriteRendererBottom.enabled = false;
        spriteRendererTop.enabled = false;
        currentLevel = 0;
    }
}
