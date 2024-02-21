using Infrastructure.Interfaces;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.GameStates
{
    internal class GameplayState: IGameState
    {
        private readonly LevelStateMachine _levelStateMachine;

        public GameplayState(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }
        public void Enter()
        {
            //Debug.Log("GameplayState: Enter");
            _levelStateMachine.EnterState<InitLevelState>();
        }

        public void Exit()
        {
            //Debug.Log("GameplayState: Exit");
        }
    }
}