using System.Collections.Generic;
using Deadspell.Services;
using Entitas;

namespace Deadspell.Systems.Services
{
    public class RegisterTooltipServiceSystem : IInitializeSystem
    {
        private readonly MetaContext _context;
        private readonly ITooltipService _tooltipService;

        public RegisterTooltipServiceSystem(Contexts contexts, ITooltipService tooltipService)
        {
            _context = contexts.meta;
            _tooltipService = tooltipService;
        }
        
        public void Initialize()
        {
            _context.ReplaceTooltipService(_tooltipService);
        }
    }
}