using Entitas;

namespace Deadspell.Systems
{
    public class CleanupInputSystem : ICleanupSystem
    {
        private readonly InputContext _context;

        public CleanupInputSystem(Contexts contexts)
        {
            _context = contexts.input;
        }

        public void Cleanup()
        {
            if (_context.leftMouseEntity.hasMouseDown)
                _context.leftMouseEntity.RemoveMouseDown();
            if (_context.leftMouseEntity.hasMouseUp)
                _context.leftMouseEntity.RemoveMouseUp();
            if (_context.rightMouseEntity.hasMouseDown)
                _context.rightMouseEntity.RemoveMouseDown();
            if (_context.rightMouseEntity.hasMouseUp)
                _context.rightMouseEntity.RemoveMouseUp();
        }
    }
}