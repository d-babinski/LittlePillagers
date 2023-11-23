using UnityEngine;
using UnityEngine.Events;

public class OnBoolChangedEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onBoolChangeToTrue = null;
    [SerializeField] private UnityEvent onBoolChangedToFalse = null;
    [SerializeField] private BoolVariable variableTracked = null;

    private bool lastValue = false;

    private void Update()
    {
        if (variableTracked.Value == lastValue)
        {
            return;
        }

        lastValue = variableTracked.Value;

        if (variableTracked.Value == true)
        {
            onBoolChangeToTrue?.Invoke();
            return;
        }

        onBoolChangedToFalse?.Invoke();
    }
}
