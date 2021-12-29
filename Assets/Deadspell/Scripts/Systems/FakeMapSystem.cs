using Deadspell.Components;
using Deadspell.Core;
using Deadspell.Data;
using Deadspell.Map;
using Deadspell.MapBuilders;
using Entitas;
using UnityEngine;

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

            /*
            var enemyTile = Resources.Load<Tile>("EnemyTile");
            var enemy = _context.CreateEntity();
            enemy.AddPosition(new Vector2Int(10, 10));
            enemy.AddRenderable(TileCache.GetTile(enemyTile.Sprite, enemyTile.Color), 1);
            enemy.AddName("Goblin");
            enemy.AddAttributes(new Attributes());
            enemy.attributes.Attributes.Might.Base = 6;
            enemy.attributes.Attributes.Agility.Base = 14;
            enemy.attributes.Attributes.Wits.Base = 8;
            enemy.attributes.Attributes.Presence.Base = 8;
            enemy.AddStats(new StatsComponent.Pool(GameSystem.PlayerHealthAtLevel(enemy.attributes.Attributes.Vitality, 1)),
                new StatsComponent.Pool(GameSystem.ManaAtLevel(enemy.attributes.Attributes.Wits, 1)),
                0,
                1,
                0,
                0,
                10,
                false);
            enemy.AddEnergy(0);
            enemy.AddFaction(GameManager.Instance.EnemyFaction);
            enemy.isBlocksTile = true;

            var chairTile = Resources.Load<Tile>("ChairTile");
            var chair = _context.CreateEntity();
            chair.AddPosition(new Vector2Int(10, 10));
            chair.AddRenderable(TileCache.GetTile(chairTile.Sprite, chairTile.Color), 2);
            chair.AddName("Chair");
            */
        }
    }
}