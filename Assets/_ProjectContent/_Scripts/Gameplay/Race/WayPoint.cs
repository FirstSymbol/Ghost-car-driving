using UnityEngine;
using Zenject;

namespace Gameplay.Race
{
  [RequireComponent(typeof(Collider))]
  public class WayPoint : MonoBehaviour
  {
    public int wayPointNumber;

    private IWaypointsService _waypointService;

    [Inject]
    private void Inject(IWaypointsService waypointService)
    {
      _waypointService = waypointService;
    }

    private void Start()
    {
      if (!_waypointService.RegisterWaypoint(wayPointNumber))
      {
#if DEV
        Debug.LogError($"Waypoint {wayPointNumber} has already been registered therefore, it will be deleted");
#endif
        Destroy(gameObject);
      }
    }

    private void OnTriggerEnter(Collider other)
    {
      _waypointService.UnlockWaypoint(wayPointNumber);
    }
  }
}