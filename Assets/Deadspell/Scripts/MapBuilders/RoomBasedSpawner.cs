using System.Collections.Generic;
using Deadspell.Data.Blueprints;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class RoomBasedSpawner : IMetaMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            Build(ref data);
        }

        private void Build(ref MapBuilder.Data data)
        {
            if (data.Rooms == null)
            {
                Debug.LogError("Nearest corridors require a builder with room structures.");
                return;
            }

            data.SpawnList ??= new List<(Vector2Int, Blueprint)>();
            for (int i = 1; i < data.Rooms.Count; i++)
            {
                var room = data.Rooms[i];
                Spawner.SpawnRoom(data.Map, room, 0, data.SpawnList);
            }
        }
    }
}