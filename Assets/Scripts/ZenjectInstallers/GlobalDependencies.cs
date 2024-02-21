using Factory;
using GameLogic.Bullet;
using GameLogic.Configs;
using GameLogic.UI;
using Infrastructure.Services;
using Infrastructure.StateMachinesInfrastructure.StateMachines;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class GlobalDependencies : MonoInstaller
    {
        [SerializeField]
        private BulletPrefabsSo _bulletPrefabsSo;
        public override void InstallBindings()
        { 
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
            Container.Bind<StateMachineFactory>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<LevelStateMachine>().AsSingle();
            Container.Bind<PlayerStats>().AsSingle();
            Container.Bind<GameControlService>().AsSingle();
            Container.Bind<PlayerStatsChangerService>().AsSingle();
            Container.Bind<BulletFactory>().AsSingle().WithArguments(_bulletPrefabsSo);
            Container.Bind<BossBattleService>().AsSingle();

        }
    }
}