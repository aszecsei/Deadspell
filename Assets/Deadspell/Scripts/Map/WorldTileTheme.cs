using System.Collections.Generic;
using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Deadspell.Map
{
    public enum WorldTileType
    {
        Grassland,
        Forest,
        Lake,
        Ocean,
        Mountain,
        Tundra,
        Town,
    }

    [CreateAssetMenu(menuName = "Deadspell/World Tile Theme")]
    public class WorldTileTheme : SerializedScriptableObject
    {
        public struct TileData
        {
            public Sprite Sprite;
            public ColorReference Color;
        }

        public TileData Default = new TileData();
        public Dictionary<WorldTileType, TileData> Data = new Dictionary<WorldTileType, TileData>();

        public TileData this[WorldTileType tileType]
        {
            get
            {
                if (Data.TryGetValue(tileType, out var result))
                {
                    return result;
                }

                return Default;
            }
            set => Data[tileType] = value;
        }
    }
}