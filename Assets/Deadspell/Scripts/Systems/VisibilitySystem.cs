using Deadspell.Map;
using Entitas;

namespace Deadspell.Systems
{
    public class VisibilitySystem : IExecuteSystem
    {
        private GameContext _context;
        private IGroup<GameEntity> _viewing;

        public VisibilitySystem(Contexts contexts)
        {
            _context = contexts.game;
            _viewing = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Viewshed, GameMatcher.Position));
        }
        
        public void Execute()
        {
            var mapData = _context.gameMap.MapData;
            
            foreach (var e in _viewing)
            {
                if (e.viewshed.Dirty)
                {
                    FieldOfView.Calculate(e.position.Position.x, e.position.Position.y, e.viewshed.Range, mapData, e.viewshed.VisibleTiles);
                    e.viewshed.Dirty = false;
                    
                    // If this is the player, reveal what they can see
                    if (e.isPlayer)
                    {
                        for (var y = 0; y < mapData.Height; y++)
                        {
                            for (var x = 0; x < mapData.Width; x++)
                            {
                                mapData[x, y].Visible = false;
                            }
                        }

                        foreach (var pos in e.viewshed.VisibleTiles)
                        {
                            if (mapData.IsInBounds(pos.x, pos.y))
                            {
                                mapData[pos.x, pos.y].Revealed = true;
                                mapData[pos.x, pos.y].Visible = true;
                            }

                            // TODO: Chance to reveal hidden things
                        }
                    }
                }
            }
        }
    }
}