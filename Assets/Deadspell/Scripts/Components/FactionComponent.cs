using System.Collections.Generic;
using Deadspell.Core;
using Deadspell.Data;
using Entitas;

namespace Deadspell.Components
{
    [Game]
    public class FactionComponent : IComponent
    {
        public List<FactionLoyalty> Factions;
    }
}