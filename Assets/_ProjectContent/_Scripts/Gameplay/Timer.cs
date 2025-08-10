using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Race;
using GlobalUI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay
{
  public class Timer : MonoBehaviour
  {
    public PlayerCar playerCar;
    public RecordText recordText;
    public TextMeshProUGUI timerText;
    
    private SVector3Int time = new SVector3Int(0, 0, 0);
    private IRaceService _raceService;
    public bool _isPaused;
    private CancellationTokenSource  _tokenSource;

    [Inject]
    private void Inject(IRaceService raceService)
    {
      _raceService = raceService;
    }

    private void Start()
    {
      playerCar.OnDeath += OnCarDeath;
      _raceService.OnRaceFinish += OnRaceFinish;
      time = _raceService.SaveData.RecordTime;
      SetTime();
      TimerStart();
    }

    private void OnCarDeath()
    {
      TimerReset();
    }

    private void OnRaceFinish()
    {
      TimerStop();
      var t1 = _raceService.SaveData.RecordTime.X * 60 * 1000 + _raceService.SaveData.RecordTime.Y * 1000 + _raceService.SaveData.RecordTime.Z;
      var t2 = time.X * 60 * 1000 + time.Y * 1000 + time.Z;
      if (t1 > t2 || t1 == 0)
      {
        _raceService.SaveData.RecordTime = time;
        recordText.SetTime();
      }
      
      TimerReset();
      TimerStart();
    }

    private void OnDestroy()
    {
      _raceService.OnRaceFinish -= OnRaceFinish;
      playerCar.OnDeath -= OnCarDeath;
    }

    public async void TimerStart()
    {
      _tokenSource?.Cancel();
      _isPaused = false;
      _tokenSource = new CancellationTokenSource();
      await PlayTimer(_tokenSource.Token);
    }

    public void TimerStop()
    {
      _isPaused = true;
      _tokenSource?.Cancel();
    }

    public void TimerReset()
    {
      time = new SVector3Int(0, 0, 0);
      SetTime();
    }

    private async UniTask PlayTimer(CancellationToken token)
    {
      await UniTask.WaitWhile(() => _isPaused, cancellationToken: token);
      while (true)
      {
        await UniTask.WaitWhile(() => _isPaused, cancellationToken: token);
        await UniTask.Yield();
        var t = Mathf.FloorToInt(Time.deltaTime * 1000);
        time.Z += t;

        if (time.Z >= 1000)
        {
          time.Y += time.Z / 1000;
          time.Z -= time.Z / 1000 * 1000;
        }
        if (time.Y >= 60)
        {
          time.X += time.Y / 60;
          time.Y -= time.Y / 60 * 60;
        }
        SetTime();
      }
    }

    private void SetTime()
    {
      timerText.text = $"{time.X:D2}:" +
                       $"{time.Y:D2}:" +
                       $"{time.Z:D3}";
    }
  }
}