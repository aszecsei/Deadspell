using Entitas;

namespace Deadspell.Systems
{
    public class CleanupDebugMessageSystem : ICleanupSystem
    {
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _debugMessages;

        public CleanupDebugMessageSystem(Contexts contexts)
        {
            _context = contexts.game;
            _debugMessages = _context.GetGroup(GameMatcher.DebugMessage);
        }

        public void Cleanup()
        {
            foreach (var e in _debugMessages.GetEntities())
            {
                e.Destroy();
            }
        }
    }
}