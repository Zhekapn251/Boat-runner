using GameLogic.Entities;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace GameLogic.Bullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particleSystems;
        [SerializeField] private BulletMover _bulletMover;
        [SerializeField] private BulletAttack _bulletAttack;
        [SerializeField] private WeaponType _weaponType;
        
        private GameControlService _gameControlService;
        private float _damage;
        private BulletPool _bulletPool;
        
        [Inject]
        public void Construct(BulletPool bulletPool, GameControlService gameControlService)
        {
            _gameControlService = gameControlService;
            _bulletPool = bulletPool;
        }
        private void Start()
        {
            _bulletAttack.SetDamageCoefficient(_damage);
        }

        private void OnEnable()
        {
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }
        
        private void OnDisable()
        {
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Stop();
            }
        }
        
        public WeaponType WeaponType => _weaponType;
        
        public void SetGameControlService(GameControlService gameControlService) =>
            _gameControlService = gameControlService;
        
        
        public void SetDamageCoefficient(float damage) =>
            _damage = damage;
        
        
        
    }
}