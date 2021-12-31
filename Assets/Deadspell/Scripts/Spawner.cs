using System;
using System.Collections.Generic;
using Deadspell.Components;
using Deadspell.Core;
using Deadspell.Data;
using Deadspell.Data.Blueprints;
using Deadspell.Map;
using UnityEngine;
using Display = Deadspell.Data.Blueprints.Display;
using Random = UnityEngine.Random;

namespace Deadspell
{
    public static class Spawner
    {
        public static void SpawnRoom(MapData map, RectInt room, int mapDepth, List<(Vector2Int, Blueprint)> spawnList)
        {
            var possibleTargets = new List<Vector2Int>();
            for (int y = room.yMin; y <= room.yMax; y++)
            {
                for (int x = room.xMin; x <= room.xMax; x++)
                {
                    if (map[x, y].Type == TileType.Floor)
                    {
                        possibleTargets.Add(new Vector2Int(x, y));
                    }
                }
            }
            SpawnRegion(map, possibleTargets, mapDepth, spawnList);
        }
        
        public static void SpawnRegion(MapData map, List<Vector2Int> area, int mapDepth, List<(Vector2Int, Blueprint)> spawnList)
        {
            // TODO: Get spawn table from map data?
            var spawnTable = Resources.Load<SpawnTable>("Spawn Tables/Default");
            if (spawnTable == null)
            {
                Debug.LogError("Unable to find default spawn table");
            }

            var spawnPoints = new Dictionary<Vector2Int, Blueprint>();
            var areas = new List<Vector2Int>(area);

            var numSpawns = Mathf.Min(area.Count, Random.Range(1, 7) - 3); // TODO
            if (numSpawns == 0)
            {
                return;
            }

            for (int i = 0; i < numSpawns; i++)
            {
                int arrayIndex = 0;
                if (areas.Count > 1)
                {
                    arrayIndex = Random.Range(0, areas.Count);
                }

                var pt = areas[arrayIndex];
                var recipe = spawnTable.Roll();
                if (recipe != null)
                {
                    spawnPoints.Add(pt, recipe);
                }
                areas.RemoveAt(arrayIndex);
            }
            
            // Actually spawn in the items and monsters
            foreach (var spawn in spawnPoints)
            {
                spawnList.Add((spawn.Key, spawn.Value));
            }
        }

        private static void SpawnPosition(GameEntity entity, Vector2Int position)
        {
            // TODO: Handle different position types (carried by, equipped by)
            entity.AddPosition(position);
        }

        public static GameEntity SpawnEntity(Blueprint recipe, GameContext context, Vector2Int position)
        {
            if (recipe == null)
            {
                Debug.LogError("Attempt to spawn null blueprint");
                return null;
            }

            var entity = context.CreateEntity();
            SpawnPosition(entity, position);

            foreach (var component in recipe.AllComponents)
            {
                component.AddToEntity(entity);
            }
            
            return entity;
        }

        /*

            enemy.AddFaction(recipe.Faction); // TODO: Fallback to mindless if this is null

            enemy.AddEnergy(2);
            if (recipe.BlocksTile)
            {
                enemy.isBlocksTile = true;
            }
            
            enemy.AddViewshed(new HashSet<Vector2Int>(), recipe.VisionRange, true);

            if (recipe.Abilities != null)
            {
                // TODO
            }

            if (recipe.OnDeath != null)
            {
                // TODO
            }

            if (recipe.Equipped != null)
            {
                // TODO
            }
            
            if (recipe.Inventory != null)
            {
                // TODO
            }
            
            if (recipe.Vendor != VendorCategory.None)
            {
                // TODO: Add vendor items to inventory
            }

            return enemy;
        }
        */
    }
}