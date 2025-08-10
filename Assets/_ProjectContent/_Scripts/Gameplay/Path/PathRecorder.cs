using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _ProjectContent._Scripts.Gameplay.Race;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  public class PathRecorder : MonoBehaviour
  {
    public Transform wheelsTransform;
    private bool _isRecording;
    private List<PathPoint> _pathpoints = new List<PathPoint>();
    
    private IPathService _pathService;
    private IRaceService _raceService;
    private CancellationTokenSource _cancellationTokenSource;

    [Inject]
    private void Inject(IPathService pathService, IRaceService raceService)
    {
      _pathService = pathService;
      _raceService = raceService;
    }

    private void OnEnable() => 
      _raceService.OnRaceFinish += StopRecording;

    private void OnDisable() => 
      _raceService.OnRaceFinish -= StopRecording;

    private async void Start()
    {
      _cancellationTokenSource = new CancellationTokenSource();
      StartRecording();
      await Recording(_cancellationTokenSource.Token);
    }

    public void StartRecording() => 
      _isRecording = true;

    public void StopRecording()
    {
      _isRecording = false;
      _pathService.SaveData.PathPoints = _pathpoints;
      _pathpoints = new List<PathPoint>();
    }

    public async UniTask Recording(CancellationToken cancellationToken)
    {
      await UniTask.WaitWhile(() => !_isRecording, cancellationToken: cancellationToken);
      while (_isRecording)
      {
        _pathpoints.Add(new PathPoint(gameObject.transform.position, gameObject.transform.eulerAngles, wheelsTransform.localEulerAngles.y));
        await UniTask.WaitForSeconds(_pathService.SaveData.CheckStateInterval, cancellationToken: _cancellationTokenSource.Token);
      }
    }

    private void OnDestroy()
    {
      _cancellationTokenSource?.Cancel();
    }
  }
}