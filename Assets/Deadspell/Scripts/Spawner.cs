using System;
using System.Collections.Generic;
using Deadspell.Map;
using UnityEngine;

namespace Deadspell
{
    public static class Spawner
    {
        public static void SpawnRoom(MapData map, RectInt room, int mapDepth, List<(Vector2Int, string)> spawnList)
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
        
        public static void SpawnRegion(MapData map, List<Vector2Int> area, int mapDepth, List<(Vector2Int, string)> spawnList)
        {
            // TODO
            // throw new NotImplementedException();
        }
    }
}