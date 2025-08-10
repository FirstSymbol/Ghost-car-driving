using UnityEngine;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay.Race
{
  [RequireComponent(typeof(Collider))]
  public class WayPoint : MonoBehaviour
  {
    public int wayPointNumber;

    private IRaceService _raceService;

    [Inject]
    private void Inject(IRaceService raceService)
    {
      _raceService = raceService;
    }

    private void Start()
    {
      if (!_raceService.RegisterWaypointState(wayPointNumber))
      {
#if DEV
        Debug.LogError($"Waypoint {wayPointNumber} has already been registered therefore, it will be deleted");
#endif
        Destroy(gameObject);
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      if (_raceService.UnlockWaypointState(wayPointNumber))
        Destroy(gameObject);
    }
  }
}