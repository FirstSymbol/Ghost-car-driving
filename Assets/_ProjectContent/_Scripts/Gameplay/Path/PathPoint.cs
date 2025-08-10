using System;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  [Serializable]
  public class PathPoint
  {
    public Vector3 Position;
    public Vector3 Rotation;
    public readonly float WheelRotation;
    
    public PathPoint(UnityEngine.Vector3 position, UnityEngine.Vector3 rotation, float wheelRotation)
    {
      Position = new Vector3(position.x, position.y, position.z);
      Rotation = new Vector3(rotation.x, rotation.y, rotation.z);
      WheelRotation = wheelRotation;
    }
    
    public UnityEngine.Vector3 GetPosition() => new (Position.X, Position.Y, Position.Z);
    public UnityEngine.Vector3 GetRotation() => new (Rotation.X, Rotation.Y, Rotation.Z);

  }
}