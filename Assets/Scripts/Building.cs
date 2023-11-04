using UnityEngine;
using UnityEngine.Serialization;

public class Building : MonoBehaviour
{
    public bool IsHouse = false;
    public bool IsLighthouse = false;
    public bool IsMinery = false;
    public bool IsTent = false;

    public Resources BuildCost = new Resources();
}
