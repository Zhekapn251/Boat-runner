using System;
using GameLogic.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace GameLogic.Bullet
{
    [RequireComponent(typeof(SphereCollider))]
    public class BulletAttack: MonoBehaviour
    {
        public event Action OnBulletHit;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private float _damageRadius;
        [SerializeField] private float _damage;
        [SerializeField] private ParticleSystem _hitEffect;
        private float _damageCoefficient = 1.0f;
        private BulletPool _bulletPool;
         private PlayerStatsChangerService _playerStatsChangerService;
         private AudioService _audioService;

         [Inject]
         public void Construct(PlayerStatsChangerService playerStatsChangerService, BulletPool bulletPool, AudioService audioService)
         {
             _audioService = audioService;
             _playerStatsChangerService = playerStatsChangerService;
             _bulletPool = bulletPool;
         }

         private void Start()
        {
            _damageCoefficient = _playerStatsChangerService.GetWeaponDamageCoefficient();
            _collider.radius = _damageRadius;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                _bulletPool.ReturnBullet(GetComponent<Bullet>());
                damageable.TakeDamage(_damage * _damageCoefficient);
                OnBulletHit?.Invoke();
                if (_hitEffect != null)
                {
                    Instantiate(_hitEffect, transform.position, Quaternion.identity);
                    _audioService.PlayHitTarget();
                }
                gameObject.SetActive(false);
            }
        }

        public void SetDamageCoefficient(float damageCoefficient) => 
            _damageCoefficient = damageCoefficient;
    }
}