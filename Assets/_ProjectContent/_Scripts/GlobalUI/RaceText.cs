using System;
using Gameplay.Race;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace GlobalUI
{
  public class RaceText : MonoBehaviour
  {
    public TextMeshProUGUI  text;
    private IRaceService _raceService;

    [Inject]
    private void Inject(IRaceService raceService) => 
      _raceService =  raceService;
    
    private void Start()
    {
      _raceService.OnRaceFinish += UpdateText;
      UpdateText();
    }

    private void UpdateText()
    {
      transform.DOShakeScale(0.3f, 0.2f);
      text.text = $"RACE {_raceService.SaveData.RaceNumber}";
    }

    private void OnDestroy()
    {
      _raceService.OnRaceFinish -= UpdateText;
    }
  }
}