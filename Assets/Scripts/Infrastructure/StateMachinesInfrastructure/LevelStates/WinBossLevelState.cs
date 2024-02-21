using Infrastructure.Interfaces;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.LevelStates
{
    public class WinBossLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;

        public WinBossLevelState(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }
        public void Enter()
        {
            Debug.Log("WinBossLevelState");
        }

        public void Exit()
        { 
            Debug.Log("Exit WinBossLevelState");
        }
    }
}