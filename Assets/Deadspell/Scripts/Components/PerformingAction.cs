using Deadspell.Actions;
using Entitas;

namespace Deadspell.Components
{
    [Game]
    public class PerformingAction : IComponent
    {
        public Action Action;
    }
}