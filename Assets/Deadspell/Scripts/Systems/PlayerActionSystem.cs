using System.Collections.Generic;
using Deadspell.Actions;
using Deadspell.Effects;
using Entitas;
using UnityEngine;

namespace Deadspell.Systems
{
    public class PlayerActionSystem : IInitializeSystem, IExecuteSystem
    {
        private GameContext _context;
        
        public int PlayerId = 0;
        private Rewired.Player _playerController;

        public PlayerActionSystem(Contexts contexts)
        {
            _context = contexts.game;
        }
        
        public void Initialize()
        {
            _playerController = Rewired.ReInput.players.GetPlayer(PlayerId);
        }

        public void Execute()
        {
            var player = _context.playerEntity;
            if (!player.isCurrentTurn)
            {
                return;
            }
            
            if (_playerController.GetButtonRepeating("CmdMoveN"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.North));
            }
            else if (_playerController.GetButtonRepeating("CmdMoveE"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.East));
            }
            else if (_playerController.GetButtonRepeating("CmdMoveS"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.South));
            }
            else if (_playerController.GetButtonRepeating("CmdMoveW"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.West));
            }
            else if (_playerController.GetButtonRepeating("CmdMoveNW"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.NorthWest));
            }
            else if (_playerController.GetButtonRepeating("CmdMoveNE"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.NorthEast));
            }
            else if (_playerController.GetButtonRepeating("CmdMoveSW"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.SouthWest));
            }
            else if (_playerController.GetButtonRepeating("CmdMoveSE"))
            {
                player.AddPerformingAction(new WalkAction(player, Direction.SouthEast));
            }
            else if (_playerController.GetButtonRepeating("CmdRunN"))
            {
                GameLog.Log("Command not implemented: run north");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdRunE"))
            {
                GameLog.Log("Command not implemented: run east");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdRunS"))
            {
                GameLog.Log("Command not implemented: run south");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdRunW"))
            {
                GameLog.Log("Command not implemented: run west");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdRunNW"))
            {
                GameLog.Log("Command not implemented: run northwest");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdRunNE"))
            {
                GameLog.Log("Command not implemented: run northeast");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdRunSW"))
            {
                GameLog.Log("Command not implemented: run southwest");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdRunSE"))
            {
                GameLog.Log("Command not implemented: run southeast");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdAttackN"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.North, true));
            }
            else if (_playerController.GetButtonRepeating("CmdAttackE"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.East, true));
            }
            else if (_playerController.GetButtonRepeating("CmdAttackS"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.South, true));
            }
            else if (_playerController.GetButtonRepeating("CmdAttackW"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.West, true));
            }
            else if (_playerController.GetButtonRepeating("CmdAttackNW"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.NorthWest, true));
            }
            else if (_playerController.GetButtonRepeating("CmdAttackNE"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.NorthEast, true));
            }
            else if (_playerController.GetButtonRepeating("CmdAttackSW"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.SouthWest, true));
            }
            else if (_playerController.GetButtonRepeating("CmdAttackSE"))
            {
                player.AddPerformingAction(new MeleeAttackAction(player, Direction.SouthEast, true));
            }
            else if (_playerController.GetButtonDown("CmdMoveU"))
            {
                GameLog.Log("Command not implemented: move up");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdMoveD"))
            {
                GameLog.Log("Command not implemented: move down");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdWait"))
            {
                player.AddPerformingAction(new WaitAction(player));
            }
            else if (_playerController.GetButtonDown("CmdWaitUntilHealed"))
            {
                GameLog.Log("Command not implemented: wait until healed");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdAutoExplore"))
            {
                GameLog.Log("Command not implemented: auto-explore");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdExamine"))
            {
                GameLog.Log("Command not implemented: examine");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdDropItem"))
            {
                GameLog.Log("Command not implemented: drop item");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdGetItem"))
            {
                GameLog.Log("Command not implemented: get item");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdThrowItem"))
            {
                GameLog.Log("Command not implemented: throw item");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdTargetSelect"))
            {
                GameLog.Log("Command not implemented: select target");
                // TODO
            }
            else if (_playerController.GetButtonRepeating("CmdCycleTargets"))
            {
                GameLog.Log("Command not implemented: cycle targets");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdFireRanged"))
            {
                GameLog.Log("Command not implemented: fire ranged weapon");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdInventory"))
            {
                GameLog.Log("Command not implemented: inventory");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdCharacter"))
            {
                GameLog.Log("Command not implemented: view character sheet");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdSkills"))
            {
                GameLog.Log("Command not implemented: view skills");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdEquipment"))
            {
                GameLog.Log("Command not implemented: view equipment");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdQuest"))
            {
                GameLog.Log("Command not implemented: view quest log");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdFactions"))
            {
                GameLog.Log("Command not implemented: view factions");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdWorldMap"))
            {
                GameLog.Log("Command not implemented: view world map");
                // TODO
            }
            else if (_playerController.GetButtonDown("CmdExitGame"))
            {
                GameLog.Log("Command not implemented: exit game");
                // TODO
            }
            else if (_playerController.GetButton("CmdShowAllTooltips"))
            {
                GameLog.Log("Command not implemented: show all tooltips");
                // TODO
            }
        }
    }
}