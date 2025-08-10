using _ProjectContent._Scripts.Gameplay.Race;
using Infrastructure.Factories;
using Infrastructure.Providers.AssetReferenceProvider;
using Infrastructure.Services.SceneLoading;
using Infrastructure.StateMachines.GameLoopStateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay
{
  public class PlayerCar : MonoBehaviour
  {
    private ISceneLoaderService _sceneLoaderService;
    private IAssetReferenceProvider _assetReferenceProvider;
    private IGameLoopStateMachineFactory _gameLoopStateMachineFactory;
    private IRaceService _raceService;
    private bool _isDeath;

    [Inject]
    private void Inject(ISceneLoaderService sceneLoaderService, IAssetReferenceProvider  assetReferenceProvider, IGameLoopStateMachineFactory gameLoopStateMachineFactory, IRaceService raceService)
    {
      _sceneLoaderService = sceneLoaderService;
      _assetReferenceProvider = assetReferenceProvider;
      _gameLoopStateMachineFactory = gameLoopStateMachineFactory;
      _raceService = raceService;
    }

    private void Update()
    {
      if (transform.position.y <= -20 && !_isDeath) 
        Death();
    }

    public async void Death()
    {
      _isDeath = true;
      _raceService.WaypointsStates.Clear();
      _raceService.SaveData.RaceNumber++;
      await _sceneLoaderService.LoadScene(_assetReferenceProvider.MenuScene, OnSceneLoaded);
    }

    public async void OnSceneLoaded() => 
      await _gameLoopStateMachineFactory.GetFrom(this).Enter<MenuState>();
  }
}