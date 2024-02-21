using GameLogic.BoatUpgradeSystem;
using Infrastructure;
using Infrastructure.Services;
using Zenject;

namespace ZenjectInstallers
{
    public class LevelSceneContext : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelBootstrapper>().AsSingle();
            Container.Bind<IUpgradeSystem>().To<UpgradeSystem>().AsSingle();
        
        }
    }
} 