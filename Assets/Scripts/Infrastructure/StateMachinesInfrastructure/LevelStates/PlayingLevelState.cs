using Infrastructure.Interfaces;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.LevelStates
{
    public class PlayingLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;
        public PlayingLevelState(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }
        public void Enter()
        {
            //Debug.Log("PlayingLevelState");
        }
        public void Exit()
        {
            //Debug.Log("PlayingLevelState");
        }
    }
}