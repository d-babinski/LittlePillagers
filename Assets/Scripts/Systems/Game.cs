using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSetup = null;
    [SerializeField] private Level levelPrefab = null;
    [SerializeField] private PlayerStateVariable playerState = null;
    [SerializeField] private ZoomStateVariable zoomState = null;

    private Level currentLevel = null;

    private void Start()
    {
        startLevel();
    }

    private void startLevel()
    {
        playerState.SetDefaultValues();
        zoomState.SetDefaultValues();
        currentLevel = Instantiate(levelPrefab);
        currentLevel.LoadLevel(levelSetup);
    }
   
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        startLevel();
    }
}
