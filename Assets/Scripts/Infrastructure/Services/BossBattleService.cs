using System;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.Services
{
    public class BossBattleService
    {
        private readonly LevelStateMachine _levelStateMachine;
        public event Action OnBattleEnd;
        public event Action OnBoatTurnStart;
        public event Action OnBossTurnStart;
        public event Action OnTurnEnd;

        private bool _isBattleActive;
        private bool _isBossMove;
        
        private float _bossHealth;
        private float _boatHealth;

        public BossBattleService(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }


        public void StartBattle()
        {
            
            _isBattleActive = true;
            StartBoatTurn();
        }

        public void EndTurn()
        {
            if (!_isBattleActive) return;
            OnTurnEnd?.Invoke();
            
            CheckBattleConditions();
            Debug.Log("Is boss move: " + _isBossMove);
            if(_isBossMove)
            {
                StartBoatTurn();
            }
            else
            {
                StartBossTurn();
            }
        }

        public void SetBossHealth(float health) => 
            _bossHealth = health;

        public void SetBoatHealth(float health) => 
            _boatHealth = health;

        private void EndBattle()
        {
            _isBattleActive = false;
            OnBattleEnd?.Invoke();
        }
        
        private void StartBoatTurn()
        {
            if (!_isBattleActive) return;
            _isBossMove = false;
            OnBoatTurnStart?.Invoke();
        }

        private void StartBossTurn()
        {
            if (!_isBattleActive) return;
            _isBossMove = true;
            OnBossTurnStart?.Invoke();
        }

        private void CheckBattleConditions()
        {
            if (_bossHealth <= 0)
            {
                _levelStateMachine.EnterState<WinBossLevelState>();
                _isBattleActive = false;
            }
            else if (_boatHealth <= 0)
            {
                _levelStateMachine.EnterState<LoseLevelState>();
                _isBattleActive = false;
            }
        }
    }
}