using Infrastructure.Services.Saving;
using UnityEngine;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  public interface IPathService : IDataSaveable<PathData>
  {
    public void Initialize();
  }
  public class PathService : MonoBehaviour, IPathService
  {
    [field: SerializeField] public float CheckStateInterval { get; private set; } = DefaultCheckStateInterval;
    /// <summary>
    /// The time interval in seconds after which a new point is saved.
    /// </summary>
    private ISaveService _saveService;

    private static readonly float DefaultCheckStateInterval = 0.05f;

    [Inject]
    public void Inject(ISaveService saveService) => 
      _saveService = saveService;

    public void Initialize()
    {
      SaveData = _saveService.Load(this) ??
                 new PathData(CheckStateInterval <= 0 ? DefaultCheckStateInterval : CheckStateInterval);
      _saveService.AddToSaveables(this);
    }

    public SaveKey SaveKey => SaveKey.PathData;
    public PathData SaveData { get; private set; }
  }
}