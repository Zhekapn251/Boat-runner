using System;
using GameLogic.Bullet;
using GameLogic.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace GameLogic.BoatLogic
{
    public class BoatAttack : MonoBehaviour
    {
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletBezierMover _bulletPrefab;

        private float _fireRate;
        private float _damageCoefficient;
        private WeaponType _weaponType;
        private PlayerStatsChangerService _playerStatsChangerService;
        private LevelStateMachine _levelStateMachine;
        private BossBattleService _bossBattleService;
        private AudioService _audioService;

        [Inject]
        public void Construct(PlayerStatsChangerService playerStatsChangerService,
            LevelStateMachine levelStateMachine, 
            BulletPool bulletPool,
            BossBattleService bossBattleService,
            AudioService audioService)
        {
            _audioService = audioService;
            _levelStateMachine = levelStateMachine;
            _playerStatsChangerService = playerStatsChangerService;
            _bulletPool = bulletPool;
            _bossBattleService = bossBattleService;
        }

        private void Start()
        {
            _playerStatsChangerService.OnFireRateCoefficient += UpdateFireRate;
            _playerStatsChangerService.OnWeaponDamageCoefficientChanged += UpdateWeaponDamageCoefficient;
            _playerStatsChangerService.OnWeaponTypeChanged += UpdateWeaponType;
            _fireRate = 1 / _playerStatsChangerService.GetFireRateCoefficient();
        }

        private void OnDestroy()
        {
            _playerStatsChangerService.OnFireRateCoefficient -= UpdateFireRate;
            _playerStatsChangerService.OnWeaponDamageCoefficientChanged -= UpdateWeaponDamageCoefficient;
            _playerStatsChangerService.OnWeaponTypeChanged -= UpdateWeaponType;
        }

        public void AttackBoss(Vector3 bossPosition)
        {
            BulletBezierMover bullet = Instantiate(_bulletPrefab); 
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            bullet.ActivateBullet(_firePoint.position, bossPosition);
            bullet.SetBossBattleService(_bossBattleService);
            _audioService.PlayGunShoot();
            bullet.gameObject.SetActive(true);
        }

        private void UpdateFireRate(float fireRate) =>
            _fireRate = 1 / fireRate;

        private void UpdateWeaponType(WeaponType weaponType) =>
            _weaponType = weaponType;

        private void UpdateWeaponDamageCoefficient(float damage) =>
            _damageCoefficient = damage;


        private void Update()
        {
            if (_levelStateMachine.CurrentState?.GetType() == typeof(PlayingLevelState))
            {
                 HandleAttack();
            }
        }

        private void HandleAttack()
        {
            _fireRate -= Time.deltaTime;

            if (Input.GetMouseButton(0) && _fireRate <= 0)
            {
                Shoot();
                _fireRate = 1 / _playerStatsChangerService.GetFireRateCoefficient();
            }
        }

        private void Shoot()
        {
            Bullet.Bullet bullet = _bulletPool.GetBullet(_weaponType);
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;
            bullet.SetDamageCoefficient(_damageCoefficient);
            _audioService.PlayGunShoot();
            bullet.gameObject.SetActive(true);
        }
    }
}