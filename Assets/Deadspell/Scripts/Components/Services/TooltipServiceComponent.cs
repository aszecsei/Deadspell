using Deadspell.Services;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Deadspell.Components.Services
{
    [Meta, Unique]
    public class TooltipServiceComponent : IComponent
    {
        public ITooltipService Instance;
    }
}