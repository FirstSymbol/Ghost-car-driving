using System;
using _ProjectContent._Scripts.Gameplay.Race;
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
      text.text = $"RACE {_raceService.SaveData.RaceNumber}"; 
    }
  }
}