using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Deadspell.Components
{
    [Game, Event(EventTarget.Self), Event(EventTarget.Self, Entitas.CodeGeneration.Attributes.EventType.Removed)]
    public class TooltipComponent : IComponent
    {
        public Vector2Int Position;
        public string Header;
        public string Content;
    }
}