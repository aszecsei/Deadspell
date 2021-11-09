using Deadspell.Systems;
using Deadspell.Systems.AI;

namespace Deadspell.Features
{
    public class GameplaySystems : Feature
    {
        public GameplaySystems(Contexts contexts) : base("Gameplay Systems")
        {
            Add(new MapIndexingSystem(contexts));
            Add(new VisibilitySystem(contexts));
            Add(new GameInitiativeSystem(contexts));
            Add(new PlayerActionSystem(contexts));
            Add(new AISystem(contexts));
            Add(new ActionResolutionSystem(contexts));
            Add(new DeathSystem(contexts));
        }
    }
}