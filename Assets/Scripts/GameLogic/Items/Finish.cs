using System;
using GameLogic.BoatLogic;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace GameLogic.Items
{
    public class Finish: MonoBehaviour
    {
        private LevelStateMachine _levelStateMachine;

        [Inject]
        private void Construct(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Boat player))
            {
                _levelStateMachine.EnterState<WinLevelState>();
            }
        }
    }
}