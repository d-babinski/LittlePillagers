using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopulationCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent = null;
    [SerializeField] private float tweenTime = 1f;

    private int currentValue = 0;
    private Tween valueTween = null;

    private void Awake()
    {
        textComponent.text = currentValue.ToString();
    }

    public void SetNewPopulation(int _targetValue)
    {
        valueTween?.Kill();

        valueTween = DOVirtual.Int(0, _targetValue, tweenTime, _value =>
            {
                currentValue = _value;
                textComponent.text = _value.ToString();
            })
            .SetUpdate(true);
    }
}
