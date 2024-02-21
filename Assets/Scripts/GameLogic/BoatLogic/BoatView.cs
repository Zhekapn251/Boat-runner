using System;
using GameLogic.Configs;
using GameLogic.Entities;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace GameLogic.BoatLogic
{
    public class BoatView: MonoBehaviour
    {
        [SerializeField] private Transform _boatViewContainer;
        [SerializeField] private BoatViewSo     _boatViewSo;

        private PlayerStatsChangerService _playerStatsChangerService;
        

        [Inject]
        public void Construct(PlayerStatsChangerService playerStatsChangerService)
        {
            _playerStatsChangerService = playerStatsChangerService;
            _playerStatsChangerService.OnModelTypeChanged += SetBoatView;
        }

        private void OnDestroy() => 
            _playerStatsChangerService.OnModelTypeChanged -= SetBoatView;

        private void SetBoatView(ModelType modelType)
        {
            //Debug.Log($"Setting boat view to {modelType}"); 
            ClearModel();
            foreach (var boatViewStructure in _boatViewSo.BoatViewStructures)
            {
                if (boatViewStructure.ModelType == modelType)
                {
                    Instantiate(boatViewStructure.Model, _boatViewContainer);
                    return;
                }
            }
        }

        private void ClearModel()
        {
            foreach (Transform child in _boatViewContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}