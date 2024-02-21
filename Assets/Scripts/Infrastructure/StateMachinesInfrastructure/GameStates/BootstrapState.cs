using Infrastructure.Interfaces;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.GameStates
{
    public class BootstrapState: IGameState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            //Debug.Log("BootstrapState: Enter");
            _gameStateMachine.EnterState<LoadingState>();
        }

        public void Exit()
        {
            //Debug.Log("BootstrapState: Exit");
        }
    }
}