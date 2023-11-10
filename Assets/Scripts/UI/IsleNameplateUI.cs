using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IsleNameplateUI : MonoBehaviour
{
    [SerializeField] private StringVariable dataSource = null;
    [SerializeField] private TextMeshProUGUI textComponent = null;
    [SerializeField] private FloatVariable tweenTime = null;
    
    private Tween nameplateTween = null;
    private string currentName = "Borg";

    private void Update()
    {
        if (dataSource.Value != currentName)
        {
            currentName = dataSource.Value;
            updateNameplate();
        }
    }

    private void updateNameplate()
    {
        nameplateTween?.Kill();
        int _stringLength = currentName.Length;

        nameplateTween = DOVirtual.Int(0, _stringLength, tweenTime.Value, _value =>
        {
            textComponent.text = currentName.Substring(0, _value);
        }).SetUpdate(true);
    }
}
