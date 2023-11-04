using UnityEngine;
using UnityEngine.UI;

public class TimeUIBar : MonoBehaviour
{
    [SerializeField] private TimeCycler timeProvider = null;
    [SerializeField] private Image barImage = null;

    private void Update()
    {
        barImage.fillAmount = timeProvider.GetCurrentCycleProgress();
    }

}
