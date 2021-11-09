using Deadspell.Map;
using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Deadspell.Components
{
    [Game, Unique]
    public class GameMapComponent : IComponent
    {
        public MapData MapData;
    }
}