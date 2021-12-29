using Deadspell.Map;

namespace Deadspell.MapBuilders
{
    public class RoomCornerRounder : IMetaMapBuilder
    {
        public void BuildMap(ref MapBuilder.Data data)
        {
            Build(ref data);
        }

        private void FillIfCorner(int x, int y, MapBuilder.Data data)
        {
            var w = data.Map.Width;
            var h = data.Map.Height;

            var neighborOffsets = new (int, int)[]
            {
                // (-1, -1),
                (-1, 0),
                // (-1, 1),
                (0, -1),
                (0, 1),
                // (1, -1),
                (1, 0),
                // (1, 1)
            };
            const int NEIGHBOR_REQUIRED = 2; // 5 if using diagonals
            
            var neighborWalls = 0;
            foreach (var (oX, oY) in neighborOffsets)
            {
                var cX = x + oX;
                var cY = y + oY;
                if (data.Map.IsInBounds(cX, cY) && data.Map[cX, cY].Type == TileType.Wall)
                {
                    neighborWalls++;
                }
            }

            if (neighborWalls == NEIGHBOR_REQUIRED)
            {
                data.Map.SetTileType(x, y, TileType.Wall);
            }
        }

        private void Build(ref MapBuilder.Data data)
        {
            foreach (var room in data.Rooms)
            {
                FillIfCorner(room.xMin + 1, room.yMin + 1, data);
                FillIfCorner(room.xMax - 1, room.yMin + 1, data);
                FillIfCorner(room.xMin + 1, room.yMax - 1, data);
                FillIfCorner(room.xMax - 1, room.yMax - 1, data);
                data.TakeSnapshot();
            }
        }
    }
}