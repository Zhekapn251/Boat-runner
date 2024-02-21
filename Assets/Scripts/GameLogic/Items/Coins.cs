using Infrastructure.Interfaces;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace GameLogic.Items
{
    public class Coins : MonoBehaviour, Interfaces.ICollectable
    {
        [SerializeField] private int _amount;
        [SerializeField] private ParticleSystem _particleSystemPrefab;
        private PlayerStatsChangerService _playerStatsChangerService;
        private AudioService _audioService;

        [Inject]
        public void Construct(PlayerStatsChangerService playerStatsChangerService, AudioService audioService)
        {
            _audioService = audioService;
            _playerStatsChangerService = playerStatsChangerService;
        }
        public void Collect(int amount)
        {
            Instantiate(_particleSystemPrefab, transform.position, Quaternion.identity);
            _playerStatsChangerService.AddCoins(amount);
            _audioService.PlayCoinsPickup();
            Destroy(gameObject);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Collect(_amount);
            }
        }
    }
}
