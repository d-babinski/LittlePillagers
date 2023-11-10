using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class PopulationCountUI : MonoBehaviour
{
    [SerializeField] private IntVariable dataSource = null;
    [SerializeField] private TextMeshProUGUI textComponent = null;
    [SerializeField] private FloatVariable tweenTime = null;

    private int currentValue = 0;
    private Tween valueTween = null;

    private void Awake()
    {
        textComponent.text = currentValue.ToString();
    }

    private void Update()
    {
        if (dataSource.Value != currentValue)
        {
            currentValue = dataSource.Value;
            UpdatePopulation();
        }
    }

    public void UpdatePopulation()
    {
        valueTween?.Kill();

        valueTween = DOVirtual.Int(0, currentValue, tweenTime.Value, _value =>
            {
                textComponent.text = _value.ToString();
            })
            .SetUpdate(true);
    }
}
