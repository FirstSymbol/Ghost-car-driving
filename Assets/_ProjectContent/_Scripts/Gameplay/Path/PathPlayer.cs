using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  public class PathPlayer : MonoBehaviour
  {
    public Transform wheelsTransform1;
    public Transform wheelsTransform2;
    
    public float unitsPerSecond = 10f;
    
    private IPathService _pathService;
    private Tween pathTween;
    private Vector3[] pathPointsRot;
    private Vector3[] pathPointsPos;
    private float t = 0;
    
    [Inject]
    private void Inject(IPathService pathService)
    {
      _pathService = pathService;
    }

    private async void Start()
    {
      await PlayPath();
    }

    public UniTask PlayPath()
    {
      pathPointsPos = new Vector3[_pathService.SaveData.PathPoints.Count];
      pathPointsRot = new Vector3[_pathService.SaveData.PathPoints.Count];
      for (int i = 0; i < _pathService.SaveData.PathPoints.Count; i++)
      {
        pathPointsPos[i] = _pathService.SaveData.PathPoints[i].GetPosition();
        pathPointsRot[i] = _pathService.SaveData.PathPoints[i].GetRotation();
      }
      //this.t = Time.time;
      transform.DOMove(_pathService.SaveData.PathPoints[0].GetPosition(),0);
      transform.DORotate(_pathService.SaveData.PathPoints[0].GetRotation(), 0);
      pathTween = gameObject.transform.DOPath(pathPointsPos, unitsPerSecond, PathType.CatmullRom, PathMode.Full3D, 10)
        .SetSpeedBased()
        .SetEase(Ease.Linear)
        .OnWaypointChange(UpdateRotation)
        .OnComplete(() => Debug.Log("Путь завершён"));
      
      return UniTask.CompletedTask;
    }

    private void UpdateRotation(int index)
    {
      if (index >= pathPointsPos.Length)
        return;
      var diff = (pathPointsPos[index + 1] - pathPointsPos[index]).magnitude/unitsPerSecond;
      var t = pathTween.Duration()/_pathService.SaveData.PathPoints.Count;
      gameObject.transform.DOLocalRotate(_pathService.SaveData.PathPoints[index+1].GetRotation(), diff)
        .SetEase(Ease.Linear);
      wheelsTransform1.DOLocalRotate(new Vector3(0,_pathService.SaveData.PathPoints[index+1].WheelRotation,0), diff).SetEase(Ease.Linear);
      wheelsTransform2.DOLocalRotate(new Vector3(0,_pathService.SaveData.PathPoints[index+1].WheelRotation,0), diff).SetEase(Ease.Linear);
    }
  }
}