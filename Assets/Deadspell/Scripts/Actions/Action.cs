using System.Collections.Generic;
using Deadspell.Effects;
using Deadspell.Map;

namespace Deadspell.Actions
{
    public abstract class Action
    {
        private IList<Effect> _effects;
        private Queue<Action> _actions;
        private GameEntity _entity;

        public GameEntity Entity => _entity;
        protected GameContext Context => Contexts.sharedInstance.game;
        protected MapData MapData => Context.gameMap.MapData;

        public Action(NotNull<GameEntity> entity)
        {
            _entity = entity;
        }

        public ActionResult Process(IList<Effect> effects, Queue<Action> actions)
        {
            _effects = effects;
            _actions = actions;

            uint cost = EnergyCost();
            ActionResult result = OnProcess();

            _effects = null;
            _actions = null;

            if (result.Success && result.Alternate == null)
            {
                if (_entity is { hasEnergy: true })
                {
                    _entity.energy.Energy += cost;
                }
            }

            return result;
        }

        public void AddEffect(NotNull<Effect> effect)
        {
            _effects.Add(effect);
        }

        public void AddAction(NotNull<Action> action)
        {
            _actions.Enqueue(action);
        }

        public static implicit operator ActionResult(Action action)
        {
            return new ActionResult(action);
        }

        protected abstract ActionResult OnProcess();

        protected virtual uint EnergyCost()
        {
            return 0;
        }
    }
}