using System.Collections.Generic;
using GameLogic.Configs;
using GameLogic.Entities;
using UnityEngine;
using Zenject;

namespace GameLogic.Bullet
{
    public class BulletFactory
    {
        private readonly DiContainer _container;
        private readonly BulletPrefabsSo _bulletPrefabsSo;
        private Dictionary<WeaponType, GameObject> _bulletPrefabs;

        public BulletFactory(DiContainer container, BulletPrefabsSo bulletPrefabsSo)
        {
            _container = container;
            _bulletPrefabsSo = bulletPrefabsSo;
            InitializeBulletPrefabs();
        }

        private void InitializeBulletPrefabs()
        {
            _bulletPrefabs = new Dictionary<WeaponType, GameObject>();
            foreach (var bulletPrefab in _bulletPrefabsSo.BulletPrefabs)
            {
                _bulletPrefabs.Add(bulletPrefab.WeaponType, bulletPrefab.Prefab);
            }
        }

        public Bullet CreateBullet(WeaponType type)
        {
            if (_bulletPrefabs.ContainsKey(type))
            {
                return _container.InstantiatePrefab(_bulletPrefabs[type]).GetComponent<Bullet>();
            }
            else
            {
                Debug.LogError("Bullet type not found: " + type);
                return null;
            }
        }
    }
}