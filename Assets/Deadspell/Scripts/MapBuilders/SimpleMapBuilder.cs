using System.Collections.Generic;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class SimpleMapBuilder : IBaseMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            BuildRooms(ref data);
        }

        private void BuildRooms(ref MapBuilder.Data data)
        {
            const int MAX_ROOMS = 30;
            const int MIN_SIZE = 6;
            const int MAX_SIZE = 10;

            List<RectInt> rooms = new List<RectInt>();

            for (var i = 0; i < MAX_ROOMS; i++)
            {
                int w = Random.Range(MIN_SIZE, MAX_SIZE);
                int h = Random.Range(MIN_SIZE, MAX_SIZE);
                int x = Random.Range(1, data.Map.Width - w) - 1;
                int y = Random.Range(1, data.Map.Height - h) - 1;

                RectInt newRoom = new RectInt(x, y, w, h);
                bool ok = true;
                foreach (var otherRoom in rooms)
                {
                    if (newRoom.Overlaps(otherRoom))
                    {
                        ok = false;
                    }
                }

                if (ok)
                {
                    rooms.Add(newRoom);
                }
            }

            data.Rooms = rooms;
        }
    }
}