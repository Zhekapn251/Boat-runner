using Factory;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LevelBootstrapper : MonoBehaviour
    {
        private LevelStateMachine _levelStateMachine;
        private StateMachineFactory _statesFactory;


        [Inject]
        public void Construct(LevelStateMachine levelStateMachine, StateMachineFactory statesFactory)
        {
            _levelStateMachine = levelStateMachine;
            _statesFactory = statesFactory;
        }

        private void Awake()
        {
            _levelStateMachine.ClearStates();
            _levelStateMachine.RegisterState(_statesFactory.Create<InitLevelState>());
            _levelStateMachine.RegisterState(_statesFactory.Create<PlayingLevelState>());
            _levelStateMachine.RegisterState(_statesFactory.Create<PauseLevelState>());
            _levelStateMachine.RegisterState(_statesFactory.Create<WinLevelState>());
            _levelStateMachine.RegisterState(_statesFactory.Create<LoseLevelState>());
            _levelStateMachine.RegisterState(_statesFactory.Create<BossFightLevelState>());
            _levelStateMachine.RegisterState(_statesFactory.Create<WinBossLevelState>());
        }
    }
}