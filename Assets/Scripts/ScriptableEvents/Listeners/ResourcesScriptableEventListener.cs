using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Listeners
{
    [AddComponentMenu(
        ScriptableEventConstants.MenuNameCustom + "/Resources Scriptable Event Listener",
        ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class ResourcesScriptableEventListener : ScriptableEventListener<Resources>
    {
    }
}
