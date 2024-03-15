using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Events
{
    [CreateAssetMenu(
        fileName = "IsleScriptableEvent",
        menuName = ScriptableEventConstants.MenuNameCustom + "/Isle Scriptable Event",
        order = ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class IslandScriptableEvent : ScriptableEvent<Island>
    {
    }
}
