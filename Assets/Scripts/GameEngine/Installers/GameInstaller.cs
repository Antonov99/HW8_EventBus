using GameEngine;
using Handlers;
using UI;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private UIService uiService;
    
    public override void InstallBindings()
    {
        Container.Bind<EventBus>().AsSingle().NonLazy();

        Container.Bind<UIService>().FromMethod(InjectUIService).AsSingle().NonLazy();

        Container.Bind<QueueManager>().AsSingle().NonLazy();
        
        InstallHandlers();
    }

    private void InstallHandlers()
    {
        Container.BindInterfacesAndSelfTo<AttackHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AttackResolveHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DealDamageHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DestroyHandler>().AsSingle().NonLazy();
    }

    private UIService InjectUIService()
    {
        Container.Inject(uiService);
        return uiService;
    }
}