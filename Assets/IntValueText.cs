using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class IntValueText : MonoBehaviour
{
    [SerializeField] private IntVariable dataSource = null;

    private TextMeshProUGUI textComponent = null; 
    private int lastValue = 0;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (dataSource.Value != lastValue)
        {
            lastValue = dataSource.Value;
            textComponent.text = dataSource.Value.ToString();
        }
    }
}
