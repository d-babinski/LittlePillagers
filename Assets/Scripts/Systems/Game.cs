using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSetup = null;
    [SerializeField] private Level levelPrefab = null;

    private Level currentLevel = null;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        currentLevel = Instantiate(levelPrefab);
        currentLevel.LoadLevel(levelSetup);
    }
}
