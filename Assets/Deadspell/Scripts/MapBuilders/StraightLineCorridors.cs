using System.Collections.Generic;
using Deadspell.Map;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class StraightLineCorridors : IMetaMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            Corridors(ref data);
        }

        private void Corridors(ref MapBuilder.Data data)
        {
            if (data.Rooms == null)
            {
                Debug.LogError("Nearest corridors require a builder with room structures.");
                return;
            }

            var connected = new HashSet<int>();
            var corridors = new List<List<Vector2Int>>();
            for (int i = 0; i < data.Rooms.Count; i++)
            {
                var room = data.Rooms[i];
                var roomDistance = new List<(int, float)>();
                var roomCenter = room.center;

                for (int j = 0; j < data.Rooms.Count; j++)
                {
                    if (i != j && !connected.Contains(j))
                    {
                        var otherRoom = data.Rooms[j];
                        var otherCenter = otherRoom.center;
                        var distance = Vector2.Distance(roomCenter, otherCenter);
                        roomDistance.Add((j, distance));
                    }
                }

                if (roomDistance.Count != 0)
                {
                    roomDistance.Sort((a, b) => a.Item2.CompareTo(b.Item2));
                    var destCenter = data.Rooms[roomDistance[0].Item1].center;
                    var line = Utils.Bresenham(Vector2Int.RoundToInt(roomCenter), Vector2Int.RoundToInt(destCenter));
                    var corridor = new List<Vector2Int>();
                    
                    foreach (var cell in line)
                    {
                        if (data.Map[cell].Type != TileType.Floor)
                        {
                            data.Map.SetTileType(cell.x, cell.y, TileType.Floor);
                            corridor.Add(cell);
                        }
                    }
                    
                    connected.Add(i);
                    data.TakeSnapshot();
                    corridors.Add(corridor);
                }
            }

            data.Corridors = corridors;
        }
    }
}