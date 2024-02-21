using Infrastructure.Interfaces;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using ZenjectInstallers;

namespace Infrastructure.StateMachinesInfrastructure.LevelStates
{
    public class LoseLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly LoseLevelUIFactory _loseLevelUIFactory;
        private readonly AudioService _audioService;

        public LoseLevelState(LevelStateMachine levelStateMachine, LoseLevelUIFactory loseLevelUIFactory, AudioService audioService)
        {
            _audioService = audioService;
            _loseLevelUIFactory = loseLevelUIFactory;
            _levelStateMachine = levelStateMachine;
        }
        public void Enter()
        {
            Debug.Log("LoseLevelState");
            _audioService.PlayLose();
            _loseLevelUIFactory.Create();
            
        }
        public void Exit()
        {
            Debug.Log("LoseLevelState");
        }
    }
}