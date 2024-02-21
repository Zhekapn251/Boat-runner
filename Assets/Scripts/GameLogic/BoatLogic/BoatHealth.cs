using System;
using Cinemachine;
using GameLogic.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace GameLogic.BoatLogic
{
    public class BoatHealth: MonoBehaviour, IDamageable
    {
        private CinemachineImpulseSource _cinemachineImpulseSource;
        private float _health;
        private PlayerStatsChangerService _playerStatsChangerService;
        private BossBattleService _bossBattleService;
        private LevelStateMachine _levelStateMachine;
        private AudioService _audioService;

        [Inject]
        public void Construct(PlayerStatsChangerService playerStatsChangerService, 
            BossBattleService bossBattleService,
            LevelStateMachine levelStateMachine, 
            AudioService audioService)
        {
            _audioService = audioService;
            _levelStateMachine = levelStateMachine;
            _bossBattleService = bossBattleService;
            _playerStatsChangerService = playerStatsChangerService;
        }

        private void Start()
        {
            _playerStatsChangerService.OnHealthChanged += UpdateHealth;
            _bossBattleService.OnTurnEnd += OnTurnEnd;
            _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void OnDestroy()
        {
            _playerStatsChangerService.OnHealthChanged -= UpdateHealth;
            _bossBattleService.OnTurnEnd -= OnTurnEnd;
        }

        private void UpdateHealth(int health) => 
            _health = health;

        private void OnTurnEnd() =>
            _bossBattleService.SetBoatHealth(_health);

        public void TakeDamage(float damage)
        {
            _health -= damage;
            _playerStatsChangerService.SubtractHealth((int)damage);
            _audioService.PlayPlayerHit();
            _cinemachineImpulseSource.GenerateImpulse();
            if (_health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _levelStateMachine.EnterState<LoseLevelState>();
        }
    }
}