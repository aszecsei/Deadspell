using System.Collections.Generic;
using Deadspell.Components;
using Deadspell.Core;
using Deadspell.Data;
using Deadspell.Map;
using Entitas;
using UnityEngine;

namespace Deadspell.Systems
{
    public class CreatePlayerSystem : IInitializeSystem
    {
        private readonly GameContext _context;

        public CreatePlayerSystem(Contexts contexts)
        {
            _context = contexts.game;
        }
        
        public void Initialize()
        {
            _context.isPlayer = true;
            var playerTile = Resources.Load<Tile>("PlayerTile");
            var player = _context.playerEntity;
            player.AddPosition(new Vector2Int(5, 5));
            player.AddRenderable(TileCache.GetTile(playerTile.Sprite, playerTile.Color), 0);
            player.AddName("Ehrye Darkspire"); // TODO
            player.AddAttributes(new Attributes());
            player.AddStats(
                new StatsComponent.Pool(GameSystem.PlayerHealthAtLevel(player.attributes.Attributes.Vitality, 1)),
                new StatsComponent.Pool(GameSystem.ManaAtLevel(player.attributes.Attributes.Wits, 1)),
                0,
                1,
                0,
                0,
                10,
                false);
            player.AddEnergy(0);
            player.AddFaction(GameManager.Instance.PlayerFaction);
            player.isBlocksTile = true;
            player.AddViewshed(new HashSet<Vector2Int>(), 8, true);
        }
    }
}