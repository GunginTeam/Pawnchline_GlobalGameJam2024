using Services.Runtime.AudioService;
using Services.Runtime.RemoteVariables;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInstancer>().To<Instancer>().AsSingle();

        InstallServices();
        InstallPackages();
    }

    private void InstallServices()
    {
        Container.Bind<IScoreService>().To<ScoreService>()
            .AsSingle();
    }
    
    private void InstallPackages()
    {
        Container.Bind<IAudioService>().To<AudioService>()
            .AsSingle()
            .NonLazy();
        
        Container.Bind<IRemoteVariablesService>().To<RemoteVariablesService>()
            .AsSingle()
            .NonLazy();
    }
}