using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private InputAction pauseAction;
    
    [SerializeField] private PauseButton pauseButton = null;
    [SerializeField] private PauseButton playButton = null;
    [SerializeField] private TextMeshProUGUI pauseText = null;

    private bool isPaused = false;

    private void Start()
    {
        pauseButton.OnButtonClicked += togglePause;
        playButton.OnButtonClicked += togglePause;
        setPaused(true);
    }

    private void OnDestroy()
    {
        pauseButton.OnButtonClicked -= togglePause;
        playButton.OnButtonClicked -= togglePause;
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    private void Update()
    {
        if (pauseAction.triggered)
        {
            togglePause();
        }
    }

    private void togglePause()
    {
        setPaused(!isPaused);
    }

    private void setPaused(bool _pauseStatus)
    {
        isPaused = _pauseStatus;
        pauseText.gameObject.SetActive(isPaused);
        setButtonVisuals(isPaused);
        setTimeScale(isPaused);
    }
    
    private void setTimeScale(bool _isPaused)
    {
        Time.timeScale = _isPaused ? 0f : 1f;
    }

    private void setButtonVisuals(bool _pauseState)
    {
        if (_pauseState)
        {
            pauseButton.SetActiveColor();
            playButton.SetInactiveColor();
            return;
        }
        
        pauseButton.SetInactiveColor();
        playButton.SetActiveColor();
    }
}
