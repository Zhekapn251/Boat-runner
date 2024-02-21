using GameLogic.Bullet;
using GameLogic.Configs;
using GameLogic.UI;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace ZenjectInstallers
{
    public class PrefabInstaller : MonoInstaller
    {
        [SerializeField] private GameBootstrapper gameBootstrapperPrefab;
        [SerializeField] private CoroutineHandler coroutineHandlerPrefab;
        [SerializeField] private LoadingCurtain loadingCurtainPrefab;
        [SerializeField] private AudioService audioServicePrefab;
        [SerializeField] private BulletPool bulletPoolPrefab;
        [SerializeField] private ShopUI shopUIPrefab;
        [SerializeField] private UpgradeItemSO upgradeItemsSO;
        [SerializeField] private LoseLevelUI LoseLevelUIPrefab;   

        public override void InstallBindings()
        {
            Container.Bind<GameBootstrapper>().FromComponentInNewPrefab(gameBootstrapperPrefab).AsSingle().NonLazy();
            Container.Bind<ICoroutineHandler>().FromComponentInNewPrefab(coroutineHandlerPrefab).AsSingle();
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(loadingCurtainPrefab).AsSingle();
            Container.Bind<AudioService>().FromComponentInNewPrefab(audioServicePrefab).AsSingle();
            Container.Bind<ShopUI>().FromComponentInNewPrefab(shopUIPrefab).AsSingle();
            Container.Bind<BulletPool>().FromComponentInNewPrefab(bulletPoolPrefab).AsSingle();
            Container.Bind<UpgradeShop>().AsSingle();
            Container.Bind<UpgradeItemSO>().FromInstance(upgradeItemsSO);
            Container.BindFactory<LoseLevelUI, LoseLevelUIFactory>()
                .FromComponentInNewPrefab(LoseLevelUIPrefab)
                .AsTransient();
        }
    }
    
    public class LoseLevelUIFactory : PlaceholderFactory<LoseLevelUI>
    {
    }
}