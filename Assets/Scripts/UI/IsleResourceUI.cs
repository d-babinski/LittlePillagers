using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleResourceUI : MonoBehaviour
{
    [SerializeField] private ResourceBarFillUI woodBar = null;
    [SerializeField] private ResourceBarFillUI wheatBar = null;
    [SerializeField] private ResourceBarFillUI ironBar = null;
    [SerializeField] private ResourceBarFillUI goldBar = null;

    public void SetValues(Resources _values, Resources _max)
    {
        woodBar.AnimateBar((float)_values.Wood/_max.Wood);
        wheatBar.AnimateBar((float)_values.Wheat/_max.Wheat);
        ironBar.AnimateBar((float)_values.Metal/_max.Metal);
        goldBar.AnimateBar((float)_values.Gold/_max.Gold);
    }

}
