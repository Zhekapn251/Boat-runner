using Factory;
using Infrastructure.StateMachinesInfrastructure.GameStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine; 
        private StateMachineFactory _statesFactory;   
        
        
        [Inject]  
        public void Construct(GameStateMachine gameStateMachine, StateMachineFactory statesFactory)
        {
            _gameStateMachine = gameStateMachine;
            _statesFactory = statesFactory;
        }
        private void Awake()
        {
            _gameStateMachine.RegisterState(_statesFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<LoadingState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<GameplayState>());
       
            _gameStateMachine.EnterState<BootstrapState>();
        }
    }
}