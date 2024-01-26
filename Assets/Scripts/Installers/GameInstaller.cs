using System;
using Services.Runtime.AudioService;
using Services.Runtime.RemoteVariables;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CardsData _cardsData;
    [SerializeField] private CardsData _charactersSprites;
    
    public override void InstallBindings()
    {
        Container.Bind<IInstancer>().To<Instancer>().AsSingle();

        InstallServices();
        InstallModels();
        InstallPackages();
    }

    private void InstallModels()
    {
        Container.BindInstance(_cardsData).AsSingle();
        Container.BindInstance(_charactersSprites).AsSingle();
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