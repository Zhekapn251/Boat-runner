using Infrastructure.StateMachinesInfrastructure.LevelStates;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

public class ShowFinishLine : MonoBehaviour
{
    [SerializeField] private GameObject finishLine;
    private LevelStateMachine _levelStateMachine;
    [Inject]
    public void Construct(LevelStateMachine levelStateMachine)
    {
        _levelStateMachine = levelStateMachine;
    }

    private void Update()
    {
        if (_levelStateMachine.CurrentState?.GetType() == typeof(WinBossLevelState))
        {
            finishLine.SetActive(true);
        }
    }
}
