using System;
using UnityEngine;

namespace Gameplay.Path
{
  [Serializable]
  public class PathPoint
  {
    public SVector3 Position;
    public SVector3 Rotation;
    public readonly float WheelRotation;

    public PathPoint(Vector3 position, Vector3 rotation, float wheelRotation)
    {
      Position = new SVector3(position.x, position.y, position.z);
      Rotation = new SVector3(rotation.x, rotation.y, rotation.z);
      WheelRotation = wheelRotation;
    }

    public Vector3 GetPosition()
    {
      return new Vector3(Position.X, Position.Y, Position.Z);
    }

    public Vector3 GetRotation()
    {
      return new Vector3(Rotation.X, Rotation.Y, Rotation.Z);
    }
  }
}