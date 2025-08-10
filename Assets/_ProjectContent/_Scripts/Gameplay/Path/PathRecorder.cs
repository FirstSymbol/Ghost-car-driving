using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  public class PathRecorder : MonoBehaviour
  {
    public Transform wheelsTransform;
    private bool _isRecording;
    
    private IPathService _pathService;

    [Inject]
    private void Inject(IPathService pathService) => 
      _pathService = pathService;

    private async void Start()
    {
      StopRecording();
      await Recording();
    }

    public void StartRecording() => 
      _isRecording = true;

    public void StopRecording() => 
      _isRecording = false;

    public async UniTask Recording()
    {
      await UniTask.WaitWhile(() => !_isRecording);
      while (_isRecording)
      {
        _pathService.SaveData.PathPoints.Push(new PathPoint(gameObject.transform.position, gameObject.transform.eulerAngles, wheelsTransform.eulerAngles.y));
        await UniTask.WaitForSeconds(_pathService.SaveData.CheckStateInterval);
      }
    }
  }
}