using Entitas;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Deadspell.Components
{
    [Game]
    public class RenderableComponent : IComponent
    {
        public TileBase Tile;
        public int Order;
    }
}