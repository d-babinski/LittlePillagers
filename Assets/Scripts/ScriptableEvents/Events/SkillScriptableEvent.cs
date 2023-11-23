using CHARK.ScriptableEvents;
using UnityEngine;

namespace ScriptableEvents.Events
{
    [CreateAssetMenu(
        fileName = "SkillScriptableEvent",
        menuName = ScriptableEventConstants.MenuNameCustom + "/Skill Scriptable Event",
        order = ScriptableEventConstants.MenuOrderCustom + 0
    )]
    internal sealed class SkillScriptableEvent : ScriptableEvent<Skill>
    {
    }
}
