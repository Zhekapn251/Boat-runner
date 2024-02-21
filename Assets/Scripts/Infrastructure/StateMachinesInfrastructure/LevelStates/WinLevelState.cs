using GameLogic.UI;
using Infrastructure.Interfaces;
using Infrastructure.StateMachinesInfrastructure.StateMachines;

namespace Infrastructure.StateMachinesInfrastructure.LevelStates
{
    public class WinLevelState : ILevelState
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly ShopUI _shopUI;
        private readonly AudioService _audioService;

        public WinLevelState(LevelStateMachine levelStateMachine, ShopUI shopUI, AudioService audioService)
        {
            _audioService = audioService;
            _levelStateMachine = levelStateMachine;
            _shopUI = shopUI;
        }
        public void Enter()
        {
            //Debug.Log("WinLevelState");
            _shopUI.ShowShop();
            _audioService.PlayWin();
        }

        public void Exit()
        { 
            //Debug.Log("Exit WinLevelState");
            _shopUI.HideShop();
        }
    }
}