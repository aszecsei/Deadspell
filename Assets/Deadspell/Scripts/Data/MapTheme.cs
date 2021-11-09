using System.Collections.Generic;
using Deadspell.Map;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Deadspell.Data
{
    [CreateAssetMenu(menuName = "Deadspell/Maps/Theme")]
    public class MapTheme : SerializedScriptableObject
    {
        public Renderable Default = new Renderable();
        public Dictionary<TileType, Renderable> Overrides = new Dictionary<TileType, Renderable>();

        public Renderable GetRenderable(TileType tile)
        {
            Renderable renderable;
            if (!Overrides.TryGetValue(tile, out renderable))
            {
                renderable = Default;
            }

            return renderable;
        }
        
        public TileBase GetTile(TileType tile, bool greyscale = false)
        {
            Renderable renderable;
            if (!Overrides.TryGetValue(tile, out renderable))
            {
                renderable = Default;
            }

            var color = renderable.Color.Value;
            if (greyscale)
            {
                color = new Color(color.grayscale, color.grayscale, color.grayscale, color.a);
            }

            return TileCache.GetTile(renderable.Sprite, color);
        }
    }
}