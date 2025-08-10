using System;
using UnityEngine;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  [Serializable]
  public class PathPoint
  {
    public readonly Vector3 Position;
    public readonly Vector3 Rotation;
    public readonly float WheelRotation;
    
    public PathPoint(Vector3 position, Vector3 rotation, float wheelRotation)
    {
      Position = position;
      Rotation = rotation;
      WheelRotation = wheelRotation;
    }
  }
}