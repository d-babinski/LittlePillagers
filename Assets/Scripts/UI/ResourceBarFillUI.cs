using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarFillUI : MonoBehaviour
{
    [SerializeField] private Image filledImage = null;
    [SerializeField] private Gradient colorByValue = new Gradient();

    [SerializeField] private float tweenTime = 0.25f;

    private Tween barTween = null;
    private float currentVal = 0f;

    private void Awake()
    {
        setImageFill(currentVal);
        setGradientColor(currentVal);
    }

    public void AnimateBar(float _newFillValue)
    {
        barTween?.Kill();

        barTween = DOVirtual.Float(0f, _newFillValue, tweenTime, _newVal =>
            {
                currentVal = _newVal;
                setImageFill(_newVal);
                setGradientColor(_newVal);
            })
            .SetUpdate(true);
    }

    private void setImageFill(float _value)
    {
        filledImage.fillAmount = _value;
    }

    private void setGradientColor(float _value)
    {
        filledImage.color = colorByValue.Evaluate(_value);
    }
}
