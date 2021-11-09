using Deadspell.Systems;

namespace Deadspell.Features
{
    public class InputSystems : Feature
    {
        public InputSystems(Contexts contexts) : base("Input Systems")
        {
            Add(new EmitInputSystem(contexts));
            Add(new CleanupInputSystem(contexts));
        }
    }
}