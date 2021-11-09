using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Deadspell.Components
{
    [Game]
    public class ViewshedComponent : IComponent
    {
        public HashSet<Vector2Int> VisibleTiles;
        public int Range;
        public bool Dirty;
    }
}