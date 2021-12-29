using System.Collections.Generic;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class BspCorridors : IMetaMapBuilder
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
            
            var corridors = new List<List<Vector2Int>>();
            for (int i = 0; i < data.Rooms.Count - 1; i++)
            {
                var room = data.Rooms[i];
                var nextRoom = data.Rooms[i + 1];
                var start = room.RandomPoint();
                var end = nextRoom.RandomPoint();
                var corridor = Utils.DrawCorridor(data.Map, start, end);
                corridors.Add(corridor);
                data.TakeSnapshot();
            }

            data.Corridors = corridors;
        }
    }
}