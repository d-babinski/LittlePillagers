using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Events
{
    [CreateAssetMenu(
        fileName = "ResourcesScriptableEvent",
        menuName = ScriptableEventConstants.MenuNameCustom + "/Resources Scriptable Event",
        order = ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class ResourcesScriptableEvent : ScriptableEvent<Resources>
    {
    }
}
