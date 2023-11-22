using UnityEngine;
using UnityEngine.Events;

public class CompareIsle : MonoBehaviour
{
    [SerializeField] private Island compared = null;
    [SerializeField] private IslandVariable compareToScriptable = null;

    public UnityEvent OnTrueActions = null;
    public UnityEvent OnFalseActions = null;

    public void Compare(Island _compareTo)
    {
        if (compared == _compareTo)
        {
            OnTrueActions?.Invoke();
            return;
        }
        
        OnFalseActions?.Invoke();
    }

    public void CompareToScriptable()
    {
        Compare(compareToScriptable.Value);
    }
}
