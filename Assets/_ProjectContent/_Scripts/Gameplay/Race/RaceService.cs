using System;
using Infrastructure.Factories;
using Infrastructure.Providers.AssetReferenceProvider;
using Infrastructure.Services.Saving;
using Infrastructure.Services.SceneLoading;
using Infrastructure.StateMachines.GameLoopStateMachine;
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
    private IWaypointsService _waypointService;
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
      _waypointService = waypointService;
    }

    public void Initialize()
    {
      SaveData = _saveService.Load(this) ?? new RaceData();
      _saveService.AddToSaveables(this);
      _waypointService.OnAllWaypointsUnlocked += FinishRace;
    }

    public void FinishRace()
    {
      SaveData.RaceNumber++;
      OnRaceFinish?.Invoke();
      _saveService.StoreSaveFile();
    }

    public SaveKey SaveKey => SaveKey.RaceData;
    public RaceData SaveData { get; private set; }

    public void Dispose()
    {
      _waypointService.OnAllWaypointsUnlocked -= FinishRace;
    }
  }
}