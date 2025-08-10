using System;
using System.Collections.Generic;

namespace Gameplay.Path
{
  [Serializable]
  public class PathData
  {
    /// <summary>
    ///   The time interval in seconds after which a new point is saved.
    /// </summary>
    public readonly float CheckStateInterval;

    public PathData(float checkStateInterval, List<PathPoint> pathPoints = null)
    {
      PathPoints = new List<PathPoint>(pathPoints ?? new List<PathPoint>());
      CheckStateInterval = checkStateInterval;
    }

    public List<PathPoint> PathPoints { get; set; }
  }
}