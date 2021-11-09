using System.Collections.Generic;
using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Deadspell.Map
{
    [CreateAssetMenu(menuName = "Deadspell/World Tile")]
    public class WorldTile : Tile
    {
        public WorldTileTheme Theme;
        public WorldTileType TileType;
        public string Name;
        public ColorReference ColorOverride;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.sprite = Theme[TileType].Sprite;
            if (ColorOverride != null)
            {
                tileData.color = ColorOverride;
            }
            else
            {
                tileData.color = Theme[TileType].Color;
            }
        }
    }
}

