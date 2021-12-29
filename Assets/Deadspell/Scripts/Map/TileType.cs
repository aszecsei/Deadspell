namespace Deadspell.Map
{
    public enum TileType
    {
        Wall,
        Floor,
        Road,
        Grass,
        ShallowWater,
        DeepWater,
        DownStairs,
        UpStairs,
    }

    public static class TileTypeExt
    {
        public static bool Walkable(this TileType tt)
        {
            return tt switch
            {
                TileType.Floor => true,
                TileType.Road => true,
                TileType.Grass => true,
                TileType.ShallowWater => true,
                TileType.DownStairs => true,
                TileType.UpStairs => true,
                _ => false,
            };
        }

        public static bool Opaque(this TileType tt)
        {
            return tt switch
            {
                TileType.Wall => true,
                _ => false,
            };
        }

        public static float Cost(this TileType tt)
        {
            return tt switch
            {
                TileType.Road => 0.8f,
                TileType.Grass => 1.1f,
                TileType.ShallowWater => 1.2f,
                _ => 1.0f,
            };
        }
    }
}