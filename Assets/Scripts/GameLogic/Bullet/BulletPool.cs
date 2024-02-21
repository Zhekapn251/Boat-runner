using System.Collections.Generic;
using GameLogic.Configs;
using GameLogic.Entities;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace GameLogic.Bullet
{
    public class BulletPool: MonoBehaviour
    {
        [SerializeField] private BulletPrefabsSo _bulletPrefabsSo;
        [SerializeField] private int poolSize = 10;
        private Queue<Bullet> bullets = new Queue<Bullet>();
        private GameControlService _gameControlService;
        private PlayerStatsChangerService _playerStatsChangerService;
        private BulletFactory _bulletFactory;

        [Inject]
        public void Construct(GameControlService gameControlService, 
            PlayerStatsChangerService playerStatsChangerService,
            BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
            _playerStatsChangerService = playerStatsChangerService;
            _gameControlService = gameControlService;
        }
        private void Start()
        {
            
            for (int i = 0; i < poolSize; i++)
            {
                WeaponType weaponType = _playerStatsChangerService.GetWeaponType();
                Bullet bullet = CreateBullet(weaponType);
                bullets.Enqueue(bullet);
            }
        }

        public Bullet GetBullet(WeaponType weaponType)
        {
            if (bullets.Count > 0)
            {
                Bullet bullet = bullets.Dequeue();
                bullet.gameObject.SetActive(false);
                if(bullet.WeaponType != weaponType)
                {
                    bullet = CreateBullet(weaponType);
                }
                
                return bullet;
            }
            else
            {
                var bullet = CreateBullet(weaponType);
                return bullet;
            }
        }

        private Bullet CreateBullet(WeaponType weaponType)
        {
            Bullet bullet = _bulletFactory.CreateBullet(weaponType);
            bullet.gameObject.SetActive(false);
            bullet.SetGameControlService(_gameControlService);
            return bullet;
        }
 

        public void ReturnBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullets.Enqueue(bullet);
        }
    } 
}