using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Deadspell.Map
{
    public static class TileCache
    {
        private static Dictionary<Tuple<Sprite, Color>, Tile> _cache =
            new Dictionary<Tuple<Sprite, Color>, Tile>();

        public static Tile GetTile(Sprite sprite, Color color)
        {
            Tuple<Sprite, Color> key = new Tuple<Sprite, Color>(sprite, color);
            if (_cache.TryGetValue(key, out var tile))
            {
                return tile;
            }

            var t = ScriptableObject.CreateInstance<Tile>();
            t.colliderType = Tile.ColliderType.None;
            t.sprite = sprite;
            t.color = color;
            _cache[key] = t;

            return t;
        }
    }
}