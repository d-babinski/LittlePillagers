using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Listeners
{
    [AddComponentMenu(
        ScriptableEventConstants.MenuNameCustom + "/Isle Scriptable Event Listener",
        ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class IslandScriptableEventListener : ScriptableEventListener<Island>
    {
    }
}
