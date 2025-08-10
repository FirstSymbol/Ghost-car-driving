using System;
using Gameplay.Path;
using Gameplay.Race;
using UnityEngine;
using Zenject;

namespace Gameplay
{
  public class Ghost : MonoBehaviour
  {
    public PathPlayer pathPlayer;
    private IPathService _pathService;
    public Action<GameObject> OnDeath;

    private void Start()
    {
      pathPlayer.OnPathPlayComplete += Death;
    }

    private void Update()
    {
      if (transform.position.y <= -20) Death();
    }

    [Inject]
    private void Inject(IPathService pathService, IRaceService raceService)
    {
      _pathService = pathService;
    }

    private void Death()
    {
      OnDeath?.Invoke(gameObject);
      Destroy(gameObject);
    }

    private void OnDestroy()
    {
      pathPlayer.OnPathPlayComplete -= Death;
    }
  }
}