using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] private BoolVariable isPaused = null;

    private void Start()
    {
        setPaused(true);
    }

    public void TogglePause()
    {
        setPaused(!isPaused.Value);
    }

    private void setPaused(bool _pauseStatus)
    {
        isPaused.Value = _pauseStatus;
        Time.timeScale = _pauseStatus ? 0f : 1f;
    }
}
