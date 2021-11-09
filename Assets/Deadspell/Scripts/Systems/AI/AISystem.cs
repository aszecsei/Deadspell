using Deadspell.Actions;
using Entitas;

namespace Deadspell.Systems.AI
{
    public class AISystem : IExecuteSystem
    {
        private GameContext _context;
        private IGroup<GameEntity> _aiEntities;

        public AISystem(Contexts contexts)
        {
            _context = contexts.game;
            _aiEntities = _context.GetGroup(GameMatcher.AllOf(GameMatcher.CurrentTurn).NoneOf(GameMatcher.Player));
        }
        
        public void Execute()
        {
            foreach (var e in _aiEntities.GetEntities())
            {
                e.AddPerformingAction(new WaitAction(e));
            }
        }
    }
}