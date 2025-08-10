using System;
using System.Collections.Generic;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  [Serializable]
  public class PathData
  {
    public Stack<PathPoint> PathPoints { get; private set; }
    /// <summary>
    /// The time interval in seconds after which a new point is saved.
    /// </summary>
    public readonly float CheckStateInterval;

    public PathData(float checkStateInterval)
    {
      PathPoints = new Stack<PathPoint>();
      CheckStateInterval = checkStateInterval;
    }
  }
}