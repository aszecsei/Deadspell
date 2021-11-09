using System.Collections.Generic;
using Deadspell.Services;
using Entitas;

namespace Deadspell.Systems
{
    public class PlayerInfoRenderSystem : IExecuteSystem
    {
        private MetaContext _meta;
        private GameContext _context;

        public PlayerInfoRenderSystem(Contexts contexts)
        {
            _context = contexts.game;
            _meta = contexts.meta;
        }
        
        public void Execute()
        {
            var player = _context.playerEntity;
            
            _meta.uIService.Instance.SetCharacterDetails(new IUIService.CharacterDetails
            {
                Name = player.name.Name,
                Level = player.stats.Level,
                CurrentHealth = player.stats.Health.Current,
                MaxHealth = player.stats.Health.Max,
                CurrentMana = player.stats.Mana.Current,
                MaxMana = player.stats.Mana.Max,
            });
            
            _meta.uIService.Instance.SetCharacterLocation(_context.gameMap.MapData.Name);
        }
    }
}