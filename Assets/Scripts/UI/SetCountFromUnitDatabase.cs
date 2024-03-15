using TMPro;
using UnityEngine;

public class SetCountFromUnitDatabase : MonoBehaviour
{
    [SerializeField] private UnitTypeVariable unitType = null;
    [SerializeField] private Army database = null;
    [SerializeField] private TextMeshProUGUI textComponent = null;

    private int lastValue = -1;
    
    private void Update()
    {
        int _valueFromDatabase = database.UnitCountDictionary.ContainsKey(unitType.Value) ? database.UnitCountDictionary[unitType.Value] : 0;

        if (lastValue == _valueFromDatabase)
        {
            return;
        }

        lastValue = _valueFromDatabase;
        textComponent.text = _valueFromDatabase.ToString();
    }
}
