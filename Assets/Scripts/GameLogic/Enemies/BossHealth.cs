using System;
using GameLogic.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace GameLogic.Enemies
{
    public class BossHealth: MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private ParticleSystem[] _deathEffects;
        private float _health;
        private BossBattleService _bossBattleService;
        private LevelStateMachine _levelStateMachine;
        private ICoroutineHandler _coroutineHandler;
        private float _destroyDelay = 2f;
        private AudioService _audioService;

        [Inject]
        public void Construct(BossBattleService bossBattleService, ICoroutineHandler coroutineHandler, LevelStateMachine levelStateMachine, AudioService audioService)
        {
            _audioService = audioService;
            _bossBattleService = bossBattleService;
            _levelStateMachine = levelStateMachine;
            _coroutineHandler = coroutineHandler;
        }
        
        private void Start()
        {
            _health = _maxHealth;
            _bossBattleService.OnTurnEnd += UpdateHealth;
        }
        
        private void OnDestroy() => 
            _bossBattleService.OnTurnEnd -= UpdateHealth;

        private void UpdateHealth() => 
            _bossBattleService.SetBossHealth(_health);

        public void TakeDamage(float damage)
        {
            Debug.Log("BossHealth: TakeDamage" + damage);
            _health -= damage;
            if (_health <= 0)
            {
              Die();
            }
        }

        public void Die()
        {
            _audioService.PlayEnemyDeath();
            foreach (var effect in _deathEffects)
            {
                effect.Play();
            }
            _coroutineHandler.PerformAfterDelay(_destroyDelay,() =>
            {
                _levelStateMachine.EnterState<WinBossLevelState>();
            });
        }
    }
}