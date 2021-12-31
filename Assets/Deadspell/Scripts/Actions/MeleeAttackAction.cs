using Deadspell.Data;
using UnityEngine;

namespace Deadspell.Actions
{
    public class MeleeAttackAction : Action
    {
        private Direction _direction;
        private bool _checkForCancel;
        private bool _forceAttack;
        
        public MeleeAttackAction(NotNull<GameEntity> entity, Direction direction, bool forceAttack = false, bool checkForCancel = false) : base(entity)
        {
            _direction = direction;
            _checkForCancel = checkForCancel;
            _forceAttack = forceAttack;
        }

        protected override ActionResult OnProcess()
        {
            Vector2Int targetPos = Entity.position.Position + _direction.ToVector2Int();
            foreach (var e in MapData.Spatial.TileContent(targetPos.x, targetPos.y))
            {
                // TODO
                /*
                if ((Entity.hasFaction && e.hasFaction && e.faction.Faction.ResponseTo(Entity.faction.Faction) == Faction.Response.Attack) || _forceAttack)
                {
                    if (Contexts.sharedInstance.game.gameMap.MapData[Entity.position.Position.x, Entity.position.Position.y]
                        .Visible)
                    {
                        int damage = Random.Range(1, 6);
                        GameLog.Log($"{Entity.name.Name} attacks the {e.name.Name} for {damage} damage.");

                        e.stats.Health.Current -= damage;

                        return ActionResult.Done;
                    }
                }
                */
            }
            
            if (Contexts.sharedInstance.game.gameMap.MapData[Entity.position.Position.x, Entity.position.Position.y]
                .Visible)
            {
                GameLog.Log($"{Entity.name.Name} tries to attack, but there's nothing there.");
            }
            
            return ActionResult.Fail;
        }

        protected override uint EnergyCost()
        {
            return 100;
        }
    }
}