using Deadspell.Services;
using Entitas;

namespace Deadspell.Systems.Services
{
    public class RegisterUIServiceSystem : IInitializeSystem
    {
        private readonly MetaContext _context;
        private readonly IUIService _uiService;

        public RegisterUIServiceSystem(Contexts contexts, IUIService uiService)
        {
            _context = contexts.meta;
            _uiService = uiService;
        }
        
        public void Initialize()
        {
            _context.ReplaceUIService(_uiService);
        }
    }
}