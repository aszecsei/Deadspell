using Entitas;

namespace Deadspell.Systems
{
    public class MapIndexingSystem : IExecuteSystem
    {
        private GameContext _context;
        private IGroup<GameEntity> _positioned;

        public MapIndexingSystem(Contexts contexts)
        {
            _context = contexts.game;
            _positioned = _context.GetGroup(GameMatcher.Position);
        }
        
        public void Execute()
        {
            var mapData = _context.gameMap.MapData;
            mapData.Spatial.Clear();
            mapData.Spatial.PopulateBlockedFromMap(mapData);
            mapData.Spatial.PopulateOpaqueFromMap(mapData);
            
            foreach (var e in _positioned)
            {
                if (e.hasStats)
                {
                    if (e.stats.Health.Current <= 0)
                    {
                        // They're dead; don't block anything.
                        continue;
                    }
                }
                mapData.Spatial.IndexEntityAt(e, e.position.Position.x, e.position.Position.y, e.isBlocksTile, e.isOpaque);
            }
        }
    }
}