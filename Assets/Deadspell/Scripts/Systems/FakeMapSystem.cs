using Deadspell.MapBuilders;
using Entitas;

namespace Deadspell.Systems
{
    public class FakeMapSystem : IInitializeSystem
    {
        private readonly GameContext _context;

        public FakeMapSystem(Contexts contexts)
        {
            _context = contexts.game;
        }
        
        public void Initialize()
        {
            var mapBuilder = MapBuilder.ExampleMapBuilder();
            var mapData = mapBuilder.Build();
            _context.CreateEntity().AddGameMap(mapData.Map, mapData.StartingPosition!.Value);

            foreach (var (pos, recipe) in mapData.SpawnList)
            {
                Spawner.SpawnEntity(recipe, _context, pos);
            }
        }
    }
}