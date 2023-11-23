using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Listeners
{
    [AddComponentMenu(
        ScriptableEventConstants.MenuNameCustom + "/Skill Scriptable Event Listener",
        ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class SkillScriptableEventListener : ScriptableEventListener<Skill>
    {
    }
}
