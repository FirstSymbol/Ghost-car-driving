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

namespace _ProjectContent._Scripts.Gameplay.Race
{
  public interface IRaceService : IDataSaveable<RaceData>
  {
    public Dictionary<int, bool> WaypointsStates { get; set; }
    public bool RegisterWaypointState(int waypointNumber);
    public bool UnlockWaypointState(int waypointNumber);
    public Action OnRaceFinish { get; set; } 
    public void Initialize();
  }
  public class RaceService : IRaceService
  {
    private ISaveService _saveService;
    private GameLoopStateMachine _gameloopStateMachine;
    private ISceneLoaderService _sceneLoaderService;
    private IAssetReferenceProvider _assetReferenceProvider;

    public Dictionary<int, bool> WaypointsStates { get; set; } = new Dictionary<int, bool>();
    public Action OnRaceFinish { get; set; } 
    
    [Inject]
    private void Inject(ISaveService saveService,
      IGameLoopStateMachineFactory  gameLoopStateMachineFactory,
      IAssetReferenceProvider assetReferenceProvider,
      ISceneLoaderService sceneLoaderService)
    {
      _gameloopStateMachine = gameLoopStateMachineFactory.GetFrom(this);
      _saveService = saveService;
      _assetReferenceProvider = assetReferenceProvider;
      _sceneLoaderService = sceneLoaderService;
    }

    public void Initialize()
    {
      SaveData = _saveService.Load(this) ?? new RaceData();
      _saveService.AddToSaveables(this);
    }

    public async void FinishRace()
    {
      OnRaceFinish?.Invoke();
      SaveData.RaceNumber++;
      await WaitToLoad();
      
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

    public bool RegisterWaypointState(int waypointNumber) => 
      WaypointsStates.TryAdd(waypointNumber, false);
    
    public bool UnlockWaypointState(int waypointNumber)
    {
      if (!WaypointsStates.ContainsKey(waypointNumber-1))
      {
        WaypointsStates.Remove(waypointNumber);
        if (WaypointsStates.Values.Count == 0) 
          FinishRace();
        return true;
      }
      return false;
    }

    public SaveKey SaveKey => SaveKey.RaceData;
    public RaceData SaveData { get; private set; }
  }
}