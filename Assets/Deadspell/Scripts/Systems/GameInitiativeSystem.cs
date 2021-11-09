using System;
using System.Collections.Generic;
using Deadspell.Actions;
using Deadspell.Effects;
using Entitas;

namespace Deadspell.Systems
{
    public class GameInitiativeSystem : IExecuteSystem, ICleanupSystem
    {
        private GameContext _context;
        private IGroup<GameEntity> _actionable;
        private IGroup<GameEntity> _currentTurn;

        public GameInitiativeSystem(Contexts contexts)
        {
            _context = contexts.game;
            _actionable = _context.GetGroup(GameMatcher.Energy);
            _currentTurn = _context.GetGroup(GameMatcher.CurrentTurn);
        }

        public void Execute()
        {
            uint minEnergy = UInt32.MaxValue;
            foreach (GameEntity e in _actionable)
            {
                if (e.energy.Energy < minEnergy)
                {
                    minEnergy = e.energy.Energy;
                }
            }

            foreach (GameEntity e in _actionable)
            {
                e.energy.Energy -= minEnergy;
            }
            
            foreach (GameEntity e in _actionable)
            {
                if (e.energy.Energy == 0)
                {
                    e.isCurrentTurn = true;
                    break;
                }
            }
        }

        public void Cleanup()
        {
            foreach (var e in _currentTurn.GetEntities())
            {
                e.isCurrentTurn = false;
            }
        }
    }
}