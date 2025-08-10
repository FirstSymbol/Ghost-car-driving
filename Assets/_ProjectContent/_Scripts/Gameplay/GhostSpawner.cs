using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Gameplay.Path;
using Gameplay.Race;
using Infrastructure.Providers.AssetReferenceProvider;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Gameplay
{
  public class GhostSpawner : MonoBehaviour
  {
    private IAssetReferenceProvider _assetReferenceProvider;
    private IPathService _pathService;
    private IRaceService _raceService;

    private async void Start()
    {
      if (_pathService.SaveData.PathPoints.Count != 0)
      {
        await SpawnGhost();
      }
      else
      {
        _raceService.SaveData.HasGhost = false;
        _raceService.OnRaceFinish += OnRaceFinish;
      }
    }

    private void OnDestroy()
    {
      _raceService.OnRaceFinish -= OnRaceFinish;
    }

    [Inject]
    private void Inject(IAssetReferenceProvider assetReferenceProvider, IPathService pathService,
      IRaceService raceService)
    {
      _assetReferenceProvider = assetReferenceProvider;
      _pathService = pathService;
      _raceService = raceService;
    }

    private async Task SpawnGhost()
    {
      var go = await Addressables.InstantiateAsync(_assetReferenceProvider.GhostCar);
      go.GetComponent<Ghost>().OnDeath += GhostDeath;
      go.transform.DOMove(_pathService.SaveData.PathPoints[0].GetPosition(), 0);
      go.transform.DORotate(_pathService.SaveData.PathPoints[0].GetRotation(), 0);
      _raceService.SaveData.HasGhost = true;
    }

    private async void GhostDeath(GameObject go)
    {
      go.GetComponent<Ghost>().OnDeath -= GhostDeath;
      await SpawnGhost();
    }

    private async void OnRaceFinish()
    {
      if (!_raceService.SaveData.HasGhost)
      {
        await SpawnGhost();
      }
    }
  }
}