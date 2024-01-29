using Services.Runtime.AudioService;
using Services.Runtime.RemoteVariables;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CardsData _cardsData;
    [SerializeField] private CharactersData _charactersData;
    [SerializeField] private ReactionsModel _reactionsModel;
    [SerializeField] private HumorTypeSprites _humorTypeSprites;
    [SerializeField] private TextAsset _remoteVariablesData;

    public override void InstallBindings()
    {
        Container.Bind<IInstancer>().To<Instancer>().AsSingle();
        
        InstallPackageData();
        InstallPackages();

        InstallServices();
        InstallModels();
    }

    private void InstallPackageData()
    {
        Container.BindInstance(_remoteVariablesData).AsSingle();
    }

    private void InstallPackages()
    {
        Container.Bind<IAudioService>().To<AudioService>().AsSingle().NonLazy();
        Container.Bind<IRemoteVariablesService>().To<RemoteVariablesService>().AsSingle().NonLazy();
    }

    private void InstallModels()
    {
        Container.BindInstance(_cardsData).AsSingle();
        Container.BindInstance(_charactersData).AsSingle();
        Container.BindInstance(_reactionsModel).AsSingle();
        Container.BindInstance(_humorTypeSprites).AsSingle();
    }

    private void InstallServices()
    {
        Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
        Container.Bind<ICardsService>().To<CardsService>().AsSingle();
    }
}