using Deadspell.Events;
using Entitas;

namespace Deadspell.Systems
{
    public class PlayerMovementSystem : IInitializeSystem, IExecuteSystem
    {
        public int PlayerId = 0;
        private Rewired.Player _player;
        private GameContext _context;
        
        public PlayerMovementSystem(Contexts contexts)
        {
            _context = contexts.game;
        }

        public void Initialize()
        {
            _player = Rewired.ReInput.players.GetPlayer(PlayerId);
        }

        private void DoMove(GameEntity player, int x, int y)
        {
            if (_context.gameMap.MapData.Spatial.IsBlocked(player.position.Position.x + x, player.position.Position.y + y))
            {
                return;
            }
            
            player.position.Position.x += x;
            player.position.Position.y += y;
            player.viewshed.Dirty = true;
        }
        
        public void Execute()
        {
            var player = _context.playerEntity;
            
            if (_player.GetButtonRepeating("Move Up"))
            {
                DoMove(player, 0, 1);
            }
            if (_player.GetButtonRepeating("Move Right"))
            {
                DoMove(player, 1, 0);
            }
            if (_player.GetButtonRepeating("Move Down"))
            {
                DoMove(player, 0, -1);
            }
            if (_player.GetButtonRepeating("Move Left"))
            {
                DoMove(player, -1, 0);
            }
        }
    }
}