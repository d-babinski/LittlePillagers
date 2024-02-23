using UnityEngine;
using UnityEngine.Events;

public class CheckForWinCondition : MonoBehaviour
{
    [SerializeField] private IslandRuntimeSet islands = null;
    
    [SerializeField] private UnityEvent gameWonActions = null;

    public void CheckWinCondition()
    {
        if (islands.Items.Count <= 0)
        {
            gameWonActions?.Invoke();
            return;
        }

        for (int i = 0; i < islands.Items.Count; i++)
        {
            if (islands.Items[i].AreAllStagesBeaten() == false)
            {
                return;
            }
        }
        
        gameWonActions?.Invoke();
    }
}
