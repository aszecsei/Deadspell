using Deadspell.Services;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Deadspell.Components.Services
{
    [Meta, Unique]
    public class UIServiceComponent : IComponent
    {
        public IUIService Instance;
    }
}