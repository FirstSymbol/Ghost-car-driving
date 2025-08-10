using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Gameplay.Path
{
  public class PathPlayer : MonoBehaviour
  {
    public Transform wheelsTransform1;
    public Transform wheelsTransform2;

    public float unitsPerSecond = 10f;
    
    public Action OnPathPlayComplete;
    private IPathService _pathService;
    private Vector3[] pathPointsPos;
    private Vector3[] pathPointsRot;
    private float[] wheelRotations;
    private Tween pathTween;
    private CancellationTokenSource  _tokenSource;
    private float t = 0;

    private async void Start()
    {
      _tokenSource = new CancellationTokenSource();
      await PlayPath(_tokenSource.Token);
    }

    [Inject]
    private void Inject(IPathService pathService)
    {
      _pathService = pathService;
    }

    public UniTask PlayPath(CancellationToken ct)
    {
      pathPointsPos = new Vector3[_pathService.SaveData.PathPoints.Count];
      pathPointsRot = new Vector3[_pathService.SaveData.PathPoints.Count];
      wheelRotations = new float[_pathService.SaveData.PathPoints.Count];
      for (var i = 0; i < _pathService.SaveData.PathPoints.Count; i++)
      {
        pathPointsPos[i] = _pathService.SaveData.PathPoints[i].GetPosition();
        pathPointsRot[i] = _pathService.SaveData.PathPoints[i].GetRotation();
        wheelRotations[i] = _pathService.SaveData.PathPoints[i].WheelRotation;
      }
      
      pathTween = gameObject.transform.DOPath(pathPointsPos, unitsPerSecond, PathType.CatmullRom)
        .SetSpeedBased()
        .SetEase(Ease.Linear)
        .OnWaypointChange(UpdateRotation)
        .OnComplete(CompletePath);

      return UniTask.CompletedTask;
    }

    private void OnDestroy()
    {
      _tokenSource?.Cancel();
    }

    private async void CompletePath()
    {
      OnPathPlayComplete?.Invoke();
    }

    private void UpdateRotation(int index)
    {
      if (index >= pathPointsPos.Length - 2 || gameObject == null)
        return;
      var diff = (pathPointsPos[index + 1] - pathPointsPos[index]).magnitude / unitsPerSecond;
      var t = pathTween.Duration() / _pathService.SaveData.PathPoints.Count;
      gameObject.transform.DOLocalRotate(pathPointsRot[index + 1], diff)
        .SetEase(Ease.Linear);
      wheelsTransform1.DOLocalRotate(new Vector3(0, wheelRotations[index+1], 0), diff)
        .SetEase(Ease.Linear);
      wheelsTransform2.DOLocalRotate(new Vector3(0, wheelRotations[index+1], 0), diff)
        .SetEase(Ease.Linear);
    }
  }
}