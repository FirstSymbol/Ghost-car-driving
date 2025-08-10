using System;
using Gameplay.Race;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace GlobalUI
{
  public class RecordText : MonoBehaviour
  {
    public TextMeshProUGUI  timerText;
    private SVector3Int RecordTime => _raceService.SaveData.RecordTime;
    private IRaceService _raceService;
    private bool _isPaused;

    [Inject]
    private void Inject(IRaceService raceService)
    {
      _raceService = raceService;
    }

    private void Start()
    {
      SetTime();
    }

    public void SetTime()
    {
      timerText.text = $"{RecordTime.X:D2}:" +
                       $"{RecordTime.Y:D2}:" +
                       $"{RecordTime.Z:D3}";
    }
  }
}