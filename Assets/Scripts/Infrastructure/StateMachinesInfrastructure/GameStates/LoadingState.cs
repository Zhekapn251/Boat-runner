using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;

namespace Infrastructure.StateMachinesInfrastructure.GameStates
{
    public class LoadingState: IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        private readonly GameControlService _gameControlService;
        private string _sceneName = Constants.GAME_SCENE_NAME;
     
        public LoadingState(GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, 
            GameControlService gameControlService)
        {
            _gameControlService = gameControlService;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }
        public void Enter()
        {
            ShowCurtain();
            LoadPlayerStats();
            LoadGameScene();
        }
        public void Exit()
        {
            //Debug.Log("LoadingState: Exit");
            _loadingCurtain.Hide();
        }

        public void SetSceneName(string sceneName)
        {
            _sceneName = sceneName;
        }
        private void ShowCurtain()
        {
            _loadingCurtain.Show();
        }

        private void LoadGameScene()
        {
            _sceneLoader.Load(_sceneName, OnSceneLoaded);
        }

        private void LoadPlayerStats()
        { 
        }

        private void OnSceneLoaded()
        {
            //Debug.Log("LoadingState: OnSceneLoaded");
            _gameStateMachine.EnterState<GameplayState>();
        }
    }
}