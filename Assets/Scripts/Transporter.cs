using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{
    private List<GameObject> transportedObjects = new();

    public void PutIntoTransporter(GameObject _object)
    {
        _object.SetActive(false);
        _object.transform.parent = transform;
        transportedObjects.Add(_object);
    }

    public void ReleaseAll()
    {
        transportedObjects.ForEach(_object =>
        {
            _object.SetActive(true);
            _object.transform.parent = null;
        });
        
        transportedObjects.Clear();
    }
}
