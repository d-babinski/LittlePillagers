using TMPro;
using UnityEngine;

public class ResourceBlockUI : MonoBehaviour
{
    [SerializeField] private ResourcesVariable dataSource = null;
    
    [SerializeField] private string zeroReplacementString = "-";
    [SerializeField] private TextMeshProUGUI woodCountTextComponent = null;
    [SerializeField] private TextMeshProUGUI wheatCountTextComponent = null;
    [SerializeField] private TextMeshProUGUI metalCountTextComponent = null;
    [SerializeField] private TextMeshProUGUI goldCountTextComponent = null;

    private void Update()
    {
        if (dataSource == null)
        {
            setResources(new Resources());
            return;
        }
        
        setResources(dataSource.Value);
    }

    private void setResources(Resources _resources)
    {
        setValue(woodCountTextComponent, _resources.Wood);
        setValue(wheatCountTextComponent, _resources.Wheat);
        setValue(metalCountTextComponent, _resources.Metal);
        setValue(goldCountTextComponent, _resources.Gold);
    }

    private void setValue(TextMeshProUGUI _textComponent, int _value)
    {
        if (_value == 0)
        {
            _textComponent.text = zeroReplacementString;
            return;
        }

        _textComponent.text = _value.ToString();
    }
}
