using Infrastructure.Interfaces;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.LevelStates
{
    public class PauseLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;
        public PauseLevelState(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }
        public void Enter()
        {
            Debug.Log("PauseLevelState");
        }
        public void Exit()
        {
            Debug.Log("PauseLevelState");
        }
    }
}