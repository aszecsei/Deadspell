using System.Collections.Generic;
using Deadspell.Actions;
using Deadspell.Effects;
using Entitas;

namespace Deadspell.Systems
{
    public class ActionResolutionSystem : IExecuteSystem
    {
        private GameContext _context;
        private List<Effect> _effects;
        private IGroup<GameEntity> _performing;

        public ActionResolutionSystem(Contexts contexts)
        {
            _context = contexts.game;
            _effects = new List<Effect>();
            _performing = _context.GetGroup(GameMatcher.PerformingAction);
        }

        public void Execute()
        {
            foreach (var e in _performing.GetEntities())
            {
                if (e.performingAction.Action != null)
                {
                    ProcessAction(e.performingAction.Action);
                    e.performingAction.Action = null;
                }
                
                e.RemovePerformingAction();
            }
        }
        
        private void ProcessAction(Action action)
        {
            Queue<Action> actions = new Queue<Action>();
            actions.Enqueue(action);

            while (actions.Count > 0)
            {
                _effects.Clear();
                Action a = actions.Peek();
                ActionResult result = a.Process(_effects, actions);
                while (result.Alternate != null)
                {
                    result = result.Alternate.Process(_effects, actions);
                }
                
                // remove it if complete
                if (result.IsDone)
                {
                    actions.Dequeue();
                }
                
                // handle effects
                while (_effects.Count > 0)
                {
                    var currentEffect = _effects[0];
                    _effects.RemoveAt(0);
                    
                    // TODO: Process effect
                }
            }
        }
    }
}