using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameLogic.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private Slider _levelProgressSlider;

        private int _coins;
        private int _health;
        private float _levelProgress;
        
        private GameControlService _gameControlService;
        private PlayerStatsChangerService _playerStatsChangerService;

        [Inject]
        public void Construct(GameControlService gameControlService, PlayerStatsChangerService playerStatsChangerService)
        {
            _playerStatsChangerService = playerStatsChangerService;
            _gameControlService = gameControlService;
        }

        private void Awake()
        {
            _coins = _playerStatsChangerService.GetCoins();
            _health = _playerStatsChangerService.GetHealth();
            _levelProgress = _playerStatsChangerService.GetProgressLevel();
        }


        private void OnEnable() 
        {
            _playerStatsChangerService.OnCoinsChanged += OnCoinsChanged;
            _playerStatsChangerService.OnHealthChanged += OnHealthChanged;
            _playerStatsChangerService.OnProgressLevelChanged += OnLevelProgressChanged;
        }

        private void Start() =>
            UpdateUI();

        private void OnDisable()
        { 
            _playerStatsChangerService.OnCoinsChanged -= OnCoinsChanged;
            _playerStatsChangerService.OnHealthChanged -= OnHealthChanged;
            _playerStatsChangerService.OnProgressLevelChanged -= OnLevelProgressChanged;
        }

        private void OnCoinsChanged(int coins)
        {
            //Debug.Log("Coins changed");
            _coins = coins;
            _coinsText.text = _coins.ToString();
        }

        private void OnHealthChanged(int health)
        {
            _health = health;
            _healthText.text = _health.ToString();
        }

        private void OnLevelProgressChanged(float progress)
        {
            _levelProgress = progress;
            _levelProgressSlider.value = _levelProgress;
        }

        private void UpdateUI()
        {
            _coinsText.text = _coins.ToString();
            _healthText.text = _health.ToString();
            _levelProgressSlider.value = _levelProgress;
        }
    }
}