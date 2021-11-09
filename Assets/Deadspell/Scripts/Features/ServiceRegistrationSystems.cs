using Deadspell.Systems.Services;

namespace Deadspell.Features
{
    public class ServiceRegistrationSystems : Feature
    {
        public ServiceRegistrationSystems(Contexts contexts, Services.Services services)
        {
            Add(new RegisterTooltipServiceSystem(contexts, services.Tooltips));
            Add(new RegisterUIServiceSystem(contexts, services.UI));
        }
    }
}