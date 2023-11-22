using UnityEngine;

public class AddToIslandList : MonoBehaviour
{
    public RuntimeSet<Island> RuntimeSet;

    private void OnEnable()
    {
        if (TryGetComponent(out Island _island))
        {
            RuntimeSet.Add(_island);
        }
    }

    private void OnDisable()
    {
        if (TryGetComponent(out Island _island))
        {
            RuntimeSet.Remove(_island);
        }
    }

}
