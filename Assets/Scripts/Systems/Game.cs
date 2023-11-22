using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
