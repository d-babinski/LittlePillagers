using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IsleNameplateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent = null;
    [SerializeField] private float tweenTime = 1f;
    
    private Tween nameplateTween = null;
    private string currentName = "Borg";
    
    public void SetNameplate(string _newName)
    {
        nameplateTween?.Kill();
        currentName = _newName;

        int _stringLength = currentName.Length;

        nameplateTween = DOVirtual.Int(0, _stringLength, tweenTime, _value =>
        {
            textComponent.text = currentName.Substring(0, _value);
        }).SetUpdate(true);
    }
}
