using System;
using System.Collections.Generic;
using Deadspell.Core;
using Deadspell.Map;
using Deadspell.ValueReferences;
using Entitas;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Deadspell.Systems
{
    public class TileRenderSystem : IExecuteSystem
    {
        private Tilemap _tilemap;
        private GameContext _context;
        private IGroup<GameEntity> _renderable;

        public TileRenderSystem(Contexts contexts, Tilemap tilemap)
        {
            _context = contexts.game;
            _tilemap = tilemap;
            _renderable = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Renderable, GameMatcher.Position));
        }

        public void Execute()
        {
            _tilemap.ClearAllTiles();
            
            // Draw map data
            var mapData = _context.gameMap.MapData;
            for (var y = 0; y < mapData.Height; y++)
            {
                for (var x = 0; x < mapData.Width; x++)
                {
                    if (!mapData[x, y].Visible && !mapData[x, y].Revealed)
                    {
                        continue;
                    }

                    var tile = MapThemeManager.Instance.ForestTheme.GetTile(mapData[x, y].Type, greyscale: !mapData[x, y].Visible);
                    _tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }

            var renderable = _renderable.GetEntities();
            Array.Sort(renderable, (a, b) => b.renderable.Order - a.renderable.Order);
            foreach (var e in renderable)
            {
                if (_context.isPlayer)
                {
                    var playerViewshed = _context.playerEntity.viewshed;
                    if (!playerViewshed.VisibleTiles.Contains(e.position.Position))
                    {
                        continue;
                    }
                }
                
                _tilemap.SetTile(new Vector3Int(e.position.Position.x, e.position.Position.y, 0), e.renderable.Tile);
            }

            if (_context.isPlayer)
            {
                var player = _context.playerEntity;
                var playerPos = player.position;
                var cameraZOffset = Camera.main.transform.position.z;
                Camera.main.transform.position = new Vector3(0, 0, cameraZOffset);
            
                var center = Camera.main.WorldToScreenPoint(Vector3.zero);
                var offset = center + new Vector3(0, 120, 0);
                var worldSpaceOffset = Camera.main.ScreenToWorldPoint(offset);
                Camera.main.transform.position = new Vector3(playerPos.Position.x + 0.5f - worldSpaceOffset.x, playerPos.Position.y + 0.5f - worldSpaceOffset.y, cameraZOffset);
            }
            else
            {
                var cameraZOffset = Camera.main.transform.position.z;
                Camera.main.transform.position = new Vector3(0, 0, cameraZOffset);
            
                var center = Camera.main.WorldToScreenPoint(Vector3.zero);
                var offset = center + new Vector3(0, 120, 0);
                var worldSpaceOffset = Camera.main.ScreenToWorldPoint(offset);
                var centerX = mapData.Width / 2;
                var centerY = mapData.Height / 2;
                Camera.main.transform.position = new Vector3(centerX + 0.5f - worldSpaceOffset.x, centerY + 0.5f - worldSpaceOffset.y, cameraZOffset);
            }
        }
    }
}