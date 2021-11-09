using Entitas;

namespace Deadspell.Systems
{
    public class DeathSystem : IExecuteSystem
    {
        private GameContext _context;
        private IGroup<GameEntity> _ableToDie;

        public DeathSystem(Contexts contexts)
        {
            _context = contexts.game;
            _ableToDie = _context.GetGroup(GameMatcher.Stats);
        }
        
        public void Execute()
        {
            foreach (var e in _ableToDie.GetEntities())
            {
                if (!e.stats.GodMode && e.stats.Health.Current <= 0)
                {
                    if (e.isPlayer)
                    {
                        GameLog.Log("You are <color=red>dead</color>.");
                        // TODO: Handle game over
                    }
                    else
                    {
                        GameLog.Log($"The {e.name.Name} is <color=red>dead</color>.");
                        e.Destroy();
                    }
                }
            }
        }
    }
}