using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.BoatLogic;
using GameLogic.Configs;
using GameLogic.Entities;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.GameStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace GameLogic.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private ShopUIItem _itemPrefab;
        [SerializeField] private UpgradeItemSO _upgradeItemsSo;
        [SerializeField] private Button _closeButton;

        private UpgradeShop _upgradeShop;
        private List<ShopUIItem> _shopItems = new List<ShopUIItem>();
        private List<ShopUIItem> _itemsToRemove;
        private PlayerStatsChangerService _playerStatsChangerService;
        private GameStateMachine _gameStateMachine;


        [Inject]
        public void Construct(UpgradeShop upgradeShop, 
            PlayerStatsChangerService playerStatsChangerService,
            GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _playerStatsChangerService = playerStatsChangerService;
            _upgradeShop = upgradeShop;
        }

        private void Start()
        {
            HideShop();
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            InitializeShop();
        }

        public void ShowShop()
        {
            gameObject.SetActive(true);
            InitializeShop();
        }

        public void HideShop()
        {
            gameObject.SetActive(false);
        }

        private void InitializeShop()
        {
            DestroyItems();
            foreach (var upgradeItem in _upgradeItemsSo.items.Where(upgradeItem => 
                         CheckUpgradeLevel(upgradeItem, _playerStatsChangerService.GetUpgradeLevel(upgradeItem.upgradeType))))
            {
                CreateItem(upgradeItem);
            }
            Debug.Log("InitializeShop" + _shopItems.Count);
        }
        
        private bool CheckUpgradeLevel(UpgradeItem item, int upgradeLevel) => 
            upgradeLevel < item.value.Length - 1;

        private void DestroyItems()
        {
            Debug.Log("DestroyItems" + _shopItems.Count);
            foreach (var shopItem in _shopItems)
            {
                shopItem.OnItemSelected -= SelectUpgrade;
                Destroy(shopItem.gameObject);
            }
            _shopItems.Clear();
        }

        private void CreateItem(UpgradeItem upgradeItem)
        {
            ShopUIItem item = Instantiate(_itemPrefab, _content);
            item.SetItem(upgradeItem, this, _playerStatsChangerService);
            item.OnItemSelected += SelectUpgrade;
            item.name = upgradeItem.itemName;
            _shopItems.Add(item);
        }

        private void SelectUpgrade(UpgradeItem upgradeItem)
        {
            _upgradeShop.BuyUpgrade(upgradeItem, UpdateItem);
        }

        private void OnCloseButtonClicked()
        {
            HideShop();
            var nextScene = SceneManager.GetActiveScene().name == Constants.GAMEBOSS_SCENE_NAME 
                ? Constants.GAME_SCENE_NAME : Constants.GAMEBOSS_SCENE_NAME;
            
            _gameStateMachine.GetState<LoadingState>().SetSceneName(nextScene);
            _gameStateMachine.EnterState<LoadingState>();
        }
        private void UpdateItem(UpgradeItem item)
        {
            _itemsToRemove = new List<ShopUIItem>();
            
            foreach (var shopItem in _shopItems.Where(shopItem => shopItem.name == item.itemName))
            {
                shopItem.SetItem(item, this, _playerStatsChangerService);
            }
            
            foreach (var itemToRemove in _itemsToRemove)
            {
                _shopItems.Remove(itemToRemove);
            }
        }

        public void RemoveItem(ShopUIItem shopUIItem)
        {
            _itemsToRemove.Add(shopUIItem);
        }
    }
}