using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Events
{
    [CreateAssetMenu(
        fileName = "UnitTypeScriptableEvent",
        menuName = ScriptableEventConstants.MenuNameCustom + "/Unit Type Scriptable Event",
        order = ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class UnitTypeScriptableEvent : ScriptableEvent<UnitType>
    {
    }
}
