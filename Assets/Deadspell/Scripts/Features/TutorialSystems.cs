using Deadspell.Systems;

namespace Deadspell.Features
{
    public class TutorialSystems : Feature
    {
        public TutorialSystems(Contexts contexts) : base("Tutorial Systems")
        {
            Add(new FakeMapSystem(contexts));
            Add(new CreatePlayerSystem(contexts));
            Add(new HelloWorldSystem(contexts));
            Add(new LogMouseClickSystem(contexts));
            Add(new DebugMessageSystem(contexts));
            Add(new CleanupDebugMessageSystem(contexts));
        }
    }
}