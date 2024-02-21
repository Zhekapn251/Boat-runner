using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.LevelStates
{
    public class BossFightLevelState: ILevelState 
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly BossBattleService _bossBattleService;

        public BossFightLevelState(LevelStateMachine levelStateMachine, BossBattleService bossBattleService)
        {
            _levelStateMachine = levelStateMachine;
            _bossBattleService = bossBattleService;
        }
        public void Enter()
        {
            Debug.Log("BossFightLevelState");
            //_bossBattleService.StartBattle();
        }
        public void Exit()
        {
            Debug.Log("BossFightLevelState");
        }
    }
}