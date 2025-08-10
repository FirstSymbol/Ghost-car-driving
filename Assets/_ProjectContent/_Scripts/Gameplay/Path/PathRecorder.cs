using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Race;
using UnityEngine;
using Zenject;

namespace Gameplay.Path
{
  public class PathRecorder : MonoBehaviour
  {
    public Transform wheelsTransform;
    private CancellationTokenSource _cancellationTokenSource;
    private bool _isRecording;
    private List<PathPoint> _pathpoints = new();

    private IPathService _pathService;
    private IRaceService _raceService;

    private async void Start()
    {
      _cancellationTokenSource = new CancellationTokenSource();
      StartRecording();
      await Recording(_cancellationTokenSource.Token);
    }

    private void OnEnable()
    {
      _raceService.OnRaceFinish += SavePath;
    }

    private void OnDisable()
    {
      _raceService.OnRaceFinish -= SavePath;
    }

    private void OnDestroy()
    {
      _cancellationTokenSource?.Cancel();
    }

    [Inject]
    private void Inject(IPathService pathService, IRaceService raceService)
    {
      _pathService = pathService;
      _raceService = raceService;
    }

    public void StartRecording()
    {
      _isRecording = true;
    }

    public void SavePath()
    {
      _pathService.SaveData.PathPoints = _pathpoints;
      ResetPath();
    }

    public void ResetPath()
    {
      _pathpoints = new List<PathPoint>();
    }

    public async UniTask Recording(CancellationToken cancellationToken)
    {
      await UniTask.WaitWhile(() => !_isRecording, cancellationToken: cancellationToken);
      while (_isRecording)
      {
        _pathpoints.Add(new PathPoint(gameObject.transform.position, gameObject.transform.eulerAngles,
          wheelsTransform.localEulerAngles.y));
        await UniTask.WaitForSeconds(_pathService.SaveData.CheckStateInterval,
          cancellationToken: _cancellationTokenSource.Token);
      }
    }
  }
}