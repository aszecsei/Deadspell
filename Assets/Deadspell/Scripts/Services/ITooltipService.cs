using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deadspell.Services {
    public interface ITooltipService : IEventListener
    {
        public void CreateTooltip(GameEntity entity, Vector2Int position, string header, string content);
        public void DestroyTooltip(GameEntity entity);
    }
}
