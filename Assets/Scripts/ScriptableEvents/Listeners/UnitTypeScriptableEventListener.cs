using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Listeners
{
    [AddComponentMenu(
        ScriptableEventConstants.MenuNameCustom + "/Unit Type Scriptable Event Listener",
        ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class UnitTypeScriptableEventListener : ScriptableEventListener<UnitType>
    {
    }
}
