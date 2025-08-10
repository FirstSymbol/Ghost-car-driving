using System;
using _ProjectContent._Scripts.Gameplay.Path;
using Cysharp.Threading.Tasks;
using Infrastructure.Providers.AssetReferenceProvider;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay
{
  public class RacePreloader : MonoBehaviour
  {
    public Transform SceneGroup;
    private IAssetReferenceProvider _assetReferenceProvider;
    private IPathService _pathService;

    [Inject]
    private void Inject(IAssetReferenceProvider assetReferenceProvider, IPathService pathService)
    {
      _assetReferenceProvider =  assetReferenceProvider;
      _pathService = pathService;
    }

    private async void Start()
    {
      if (_pathService.SaveData.PathPoints.Count != 0)
      {
        GameObject go = await Addressables.InstantiateAsync(_assetReferenceProvider.GhostCar);
      }
    }
  }
}