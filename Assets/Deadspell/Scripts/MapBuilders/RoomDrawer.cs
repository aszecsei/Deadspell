using Deadspell.Map;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class RoomDrawer : IMetaMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            if (data.Rooms == null)
            {
                Debug.LogError("Room drawing requires a builder with room structures.");
                return;
            }

            foreach (var room in data.Rooms)
            {
                Rectangle(ref data, room);
                data.TakeSnapshot();
            }
        }

        private void Rectangle(ref MapBuilder.Data data, RectInt room)
        {
            for (var y = room.yMin + 1; y < room.yMax; y++)
            {
                for (var x = room.xMin + 1; x < room.xMax; x++)
                {
                    if (data.Map.IsInBounds(x, y))
                    {
                        data.Map.SetTileType(x, y, TileType.Floor);
                    }
                }
            }
        }
    }
}