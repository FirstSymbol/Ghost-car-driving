using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Race
{
  public interface IWaypointsService
  {
    public Dictionary<int, bool> WaypointsStates { get;}
    public bool RegisterWaypoint(int waypointNumber);
    public bool UnlockWaypoint(int waypointNumber);
    public Action OnAllWaypointsUnlocked { get; set; }
    public void RefreshWaypoints();
  }
  public class WaypointsService : IWaypointsService
  {
    public Action OnAllWaypointsUnlocked { get; set; }

    public Dictionary<int, bool> WaypointsStates { get; set; }

    public WaypointsService()
    {
      WaypointsStates = new Dictionary<int, bool>();
    }

    public bool RegisterWaypoint(int waypointNumber) => 
      WaypointsStates.TryAdd(waypointNumber, false);
    
    public bool UnlockWaypoint(int waypointNumber)
    {
      if (!WaypointsStates.ContainsKey(waypointNumber-1)  ||
          WaypointsStates.ContainsKey(waypointNumber-1) &&
          WaypointsStates[waypointNumber-1] &&
          !WaypointsStates[waypointNumber])
      {
        Debug.Log("Waypoint Unlock " + waypointNumber);
        WaypointsStates[waypointNumber] = true;
        if (!WaypointsStates.Values.Contains(false))
        {
          OnAllWaypointsUnlocked?.Invoke();
          RefreshWaypoints();
        }
        return true;
      }
      return false;
    }

    public void RefreshWaypoints()
    {
      var t = new List<int>(WaypointsStates.Keys.ToArray());
      for (int i = 0; i < t.Count; i++) 
        WaypointsStates[t[i]] = false;
    }
  }
}