using Entitas;

namespace Deadspell.Systems
{
    public class HelloWorldSystem : IInitializeSystem
    {
        private readonly GameContext _context;

        public HelloWorldSystem(Contexts contexts)
        {
            _context = contexts.game;
        }
        
        public void Initialize()
        {
            _context.CreateEntity().AddDebugMessage("Hello, world!");
        }
    }
}