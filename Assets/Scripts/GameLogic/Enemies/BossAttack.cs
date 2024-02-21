using System;
using GameLogic.BoatLogic;
using GameLogic.Bullet;
using GameLogic.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameLogic.Enemies
{
    public class BossAttack: MonoBehaviour
    {
        [SerializeField] private Transform gun;
        [SerializeField] private Boat boat;
        [SerializeField] private BulletBezierMover _bulletPrefab;


        private BulletPool _bulletPool;
        private BossBattleService _bossBattleService;
        private AudioService _audioService;

        [Inject]
        public void Construct( BulletPool bulletPool, BossBattleService bossBattleService, AudioService audioService)
        {
            _audioService = audioService;
            _bossBattleService = bossBattleService;
            _bulletPool = bulletPool;
        }

        private void Start() => 
            _bossBattleService.OnBossTurnStart += Attack;

        private void OnDestroy() => 
            _bossBattleService.OnBossTurnStart -= Attack;

        private void Attack()
        {
            BulletBezierMover bullet = Instantiate(_bulletPrefab); 
            bullet.transform.position = gun.position;
            bullet.transform.rotation = gun.rotation;
            bullet.ActivateBullet(gun.position, boat.transform.position);
            bullet.SetBossBattleService(_bossBattleService);
            _audioService.PlayGunShoot();
            bullet.gameObject.SetActive(true);
        }
    }
}