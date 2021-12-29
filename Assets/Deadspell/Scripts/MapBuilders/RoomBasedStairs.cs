using Deadspell.Map;
using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class RoomBasedStairs : IMetaMapBuilder
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

            var stairsPosition = Vector2Int.RoundToInt(data.Rooms[data.Rooms.Count - 1].center);
            data.Map.SetTileType(stairsPosition.x, stairsPosition.y, TileType.DownStairs);
            data.TakeSnapshot();
        }
    }
}