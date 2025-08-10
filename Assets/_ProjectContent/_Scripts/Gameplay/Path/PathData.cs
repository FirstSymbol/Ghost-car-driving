using System;
using System.Collections.Generic;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  [Serializable]
  public class PathData
  {
    public List<PathPoint> PathPoints { get; set; }
    /// <summary>
    /// The time interval in seconds after which a new point is saved.
    /// </summary>
    public readonly float CheckStateInterval;

    public PathData(float checkStateInterval, List<PathPoint> pathPoints = null)
    {
      PathPoints = new List<PathPoint>(pathPoints ?? new List<PathPoint>());
      CheckStateInterval = checkStateInterval;
    }
  }
}