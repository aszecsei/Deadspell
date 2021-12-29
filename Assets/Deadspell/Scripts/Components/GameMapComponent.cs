using Deadspell.Map;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Deadspell.Components
{
    [Game, Unique]
    public class GameMapComponent : IComponent
    {
        public MapData MapData;
        public Vector2Int PlayerStart;
    }
}