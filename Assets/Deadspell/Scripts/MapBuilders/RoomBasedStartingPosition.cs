using UnityEngine;

namespace Deadspell.MapBuilders
{
    public class RoomBasedStartingPosition : IMetaMapBuilder
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

            var startPos = Vector2Int.RoundToInt(data.Rooms[0].center);
            data.StartingPosition = startPos;
        }
    }
}