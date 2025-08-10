using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.Providers.AssetReferenceProvider;
using Infrastructure.Services.Saving;
using Infrastructure.Services.SceneLoading;
using Infrastructure.StateMachines.GameLoopStateMachine;
using Infrastructure.StateMachines.GameLoopStateMachine.States;
using Zenject;

namespace Gameplay.Race
{
  public interface IRaceService : IDataSaveable<RaceData>
  {
    public Action OnRaceFinish { get; set; } 
    public void Initialize();
  }
  public class RaceService : IRaceService, IDisposable
  {
    private ISaveService _saveService;
    private GameLoopStateMachine _gameloopStateMachine;
    private ISceneLoaderService _sceneLoaderService;
    private IAssetReferenceProvider _assetReferenceProvider;
    private IWaypointsService _waypointService;

    public Dictionary<int, bool> WaypointsStates { get; set; } = new Dictionary<int, bool>();
    public Action OnRaceFinish { get; set; } 
    
    [Inject]
    private void Inject(ISaveService saveService,
      IGameLoopStateMachineFactory  gameLoopStateMachineFactory,
      IAssetReferenceProvider assetReferenceProvider,
      ISceneLoaderService sceneLoaderService,
      IWaypointsService waypointService)
    {
      _gameloopStateMachine = gameLoopStateMachineFactory.GetFrom(this);
      _saveService = saveService;
      _assetReferenceProvider = assetReferenceProvider;
      _sceneLoaderService = sceneLoaderService;
      _waypointService = waypointService;
    }

    public void Initialize()
    {
      SaveData = _saveService.Load(this) ?? new RaceData();
      _saveService.AddToSaveables(this);
      _waypointService.OnAllWaypointsUnlocked += FinishRace;
    }

    public async void FinishRace()
    {
      SaveData.RaceNumber++;
      OnRaceFinish?.Invoke();
      //await WaitToLoad();
    }

    private async UniTask WaitToLoad()
    {
      await UniTask.WaitForSeconds(1f);
      await _sceneLoaderService.LoadScene(_assetReferenceProvider.MenuScene, OnMenuSceneLoaded);
    }

    private async void OnMenuSceneLoaded()
    {
      await _gameloopStateMachine.Enter<MenuState>();
    }

    

    public SaveKey SaveKey => SaveKey.RaceData;
    public RaceData SaveData { get; private set; }

    public void Dispose()
    {
      _waypointService.OnAllWaypointsUnlocked -= FinishRace;
    }
  }
}