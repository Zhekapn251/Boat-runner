using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

public class BossFightTrigger : MonoBehaviour
{
    private LevelStateMachine _levelStateMachine;

    [Inject]
    public void  Construct(LevelStateMachine levelStateMachine)
    {
        _levelStateMachine = levelStateMachine;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            _levelStateMachine.EnterState<BossFightLevelState>();
            gameObject.SetActive(false);
        }
    }
}
