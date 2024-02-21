using GameLogic.BoatLogic;
using GameLogic.Bullet;
using GameLogic.Entities;
using UnityEngine;
using Zenject;

namespace GameLogic.Enemies
{
    public class EnemyShoot : MonoBehaviour
    {
        [SerializeField] private float _fireRate;
        [SerializeField] private Transform _bulletSpawnPoint;
        private float _nextFire;
        private bool _isShooting;
        private BulletPool _bulletPool;

        [Inject]
        public void Construct(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        private void Update()
        {
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }

        private void Shoot()
        {
            Bullet.Bullet bullet = _bulletPool.GetBullet(WeaponType.EnemyCannon);
            bullet.transform.position = _bulletSpawnPoint.position;
            bullet.transform.rotation = _bulletSpawnPoint.rotation;
            bullet.gameObject.SetActive(true);
        }
    }
}