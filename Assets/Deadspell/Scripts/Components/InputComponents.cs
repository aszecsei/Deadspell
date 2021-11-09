using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Deadspell.Components
{
    [Input, Unique]
    public class LeftMouseComponent : IComponent
    {
    }
    
    [Input, Unique]
    public class RightMouseComponent : IComponent
    {
    }
    
    [Input, Unique]
    public class MousePositionComponent : IComponent
    {
        public Vector2 Position;
    }
    
    [Input, Unique]
    public class MouseDownComponent : IComponent
    {
        public Vector2 Position;
    }
    
    [Input, Unique]
    public class MouseUpComponent : IComponent
    {
        public Vector2 Position;
    }
}