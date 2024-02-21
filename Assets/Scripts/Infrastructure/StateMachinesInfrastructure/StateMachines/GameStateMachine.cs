using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace Infrastructure.StateMachinesInfrastructure.StateMachines
{
    public class GameStateMachine: AbstractStateMachine<IGameState>
    {
        SceneLoader _sceneLoader;
        
    }
}