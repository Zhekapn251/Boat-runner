using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.LevelStates
{
    public class InitLevelState: ILevelState    
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly PlayerStatsChangerService _playerStatsChangerService;
        private AudioService _audioService;

        public InitLevelState(LevelStateMachine levelStateMachine, PlayerStatsChangerService playerStatsChangerService, AudioService audioService)
        {
            _audioService = audioService;
            _playerStatsChangerService = playerStatsChangerService;
            _levelStateMachine = levelStateMachine;
        }   
   
        public void Enter()
        {
            _playerStatsChangerService.UpdateAllData();
            _levelStateMachine.EnterState<PlayingLevelState>();
            _audioService.PlayMusic();
        }
        public void Exit()
        {
            //Debug.Log("Exit InitLevelState");
            
        }
    }
}  