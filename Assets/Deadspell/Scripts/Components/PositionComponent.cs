using Entitas;
using UnityEngine;

namespace Deadspell.Components
{
    [Game]
    public class PositionComponent : IComponent
    {
        public Vector2Int Position;
    }
}