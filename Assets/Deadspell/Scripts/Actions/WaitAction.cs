using UnityEngine;

namespace Deadspell.Actions
{
    public class WaitAction : Action
    {
        public WaitAction(NotNull<GameEntity> entity) : base(entity)
        {
        }

        protected override ActionResult OnProcess()
        {
            // TODO: Health regen?
            if (Entity.hasName)
            {
                if (Contexts.sharedInstance.game.gameMap.MapData[Entity.position.Position.x, Entity.position.Position.y]
                    .Visible)
                {
                    GameLog.Log($"{Entity.name.Name} waits.");
                }
            }
            
            return ActionResult.Done;
        }

        protected override uint EnergyCost()
        {
            return 100;
        }
    }
}