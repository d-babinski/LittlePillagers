using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] private IntVariable monthOutput = null;
    [SerializeField] private FloatVariable monthProgressPercent = null;
    [SerializeField] private float monthDuration = 25f;

    private float currentCycleTime = 0f;

    private void Start()
    {
        monthOutput.Value = 1;
    }

    private void Update()
    {
        currentCycleTime += Time.deltaTime;
        monthProgressPercent.Value = currentCycleTime/monthDuration;

        if (currentCycleTime >= monthDuration)
        {
            currentCycleTime = 0f;
            monthOutput.Value += 1;
        }
    }
}

