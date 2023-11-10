using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private FloatVariable dataSource = null;
    [SerializeField] private Image barImage = null;

    private void Update()
    {
        barImage.fillAmount = dataSource.Value;
    }

}
