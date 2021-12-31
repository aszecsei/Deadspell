using Deadspell.Core;
using Deadspell.Data;
using Deadspell.Map;
using UnityEngine;

namespace Deadspell.Actions
{
    public class WalkAction : Action
    {
        private Direction _direction;
        private bool _checkForCancel;
        
        public WalkAction(GameEntity entity, Direction direction, bool checkForCancel = false)
            : base(entity)
        {
            _direction = direction;
            _checkForCancel = checkForCancel;
        }

        protected override ActionResult OnProcess()
        {
            Vector2Int oldPos = Entity.position.Position;
            Vector2Int newPos = oldPos + _direction.ToVector2Int();
            var tileType = MapData.Tiles[newPos.x, newPos.y].Type;
            
            // TODO: Check for door
            
            // TODO: Check for enemy
            foreach (var e in MapData.Spatial.TileContent(newPos.x, newPos.y))
            {
                // TODO
                /*
                if (Entity.hasFaction && e.hasFaction &&
                    e.faction.Faction.ResponseTo(Entity.faction.Faction) == Faction.Response.Attack)
                {
                    return new MeleeAttackAction(Entity, _direction, false, _checkForCancel);
                }
                */
            }
            
            // Fail if the tile is blocked
            if (MapData.Spatial.IsBlocked(newPos.x, newPos.y))
            {
                return ActionResult.Fail;
            }
            
            // Move the entity
            Entity.position.Position = newPos;
            MapData.Spatial.MoveEntity(Entity, oldPos.x, oldPos.y, newPos.x, newPos.y);

            if (Entity.hasViewshed)
            {
                Entity.viewshed.Dirty = true;
            }

            if (_checkForCancel)
            {
                return ActionResult.CheckForCancel;
            }
            else
            {
                return ActionResult.Done;
            }
        }

        protected override uint EnergyCost()
        {
            Vector2Int oldPos = Entity.position.Position;
            var delta = _direction.ToVector2Int();
            Vector2Int newPos = oldPos + delta;
            var tileType = MapData.Tiles[newPos.x, newPos.y].Type;
            
            float multiplier = 1;
            if (delta.x + delta.y == 2)
            {
                multiplier *= 1.4f;
            }

            multiplier *= tileType.Cost(); // TODO : Make this calculation more interesting
            
            return (uint)Mathf.FloorToInt(100 * multiplier);
        }
    }
}