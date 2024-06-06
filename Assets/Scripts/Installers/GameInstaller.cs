using Handlers;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EventBus>().AsSingle().NonLazy();

        InstallHandlers();
    }

    private void InstallHandlers()
    {
        Container.BindInterfacesAndSelfTo<AttackHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AttackResolveHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DealDamageHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<DestroyHandler>().AsSingle().NonLazy();
    }
}