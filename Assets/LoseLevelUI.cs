using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.GameStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoseLevelUI : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    private PlayerStatsChangerService _playerStatsChangerService;
    private LevelStateMachine _levelStateMachine;
    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(PlayerStatsChangerService playerStatsChangerService, GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
        _playerStatsChangerService = playerStatsChangerService;
    }

    private void Start()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClicked);
    }

    private void OnRestartButtonClicked()  
    {

        _playerStatsChangerService.ResetAllData();
        _gameStateMachine.GetState<LoadingState>().SetSceneName(Constants.GAME_SCENE_NAME);
        _gameStateMachine.EnterState<LoadingState>(); 
        Destroy(gameObject);
    }
}