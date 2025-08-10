using System;
using System.Numerics;

[Serializable]
public struct SVector3
{
  public float X;
  public float Y;
  public float Z;
  public float Sum => X + Y + Z;
  public SVector3(float x, float y, float z)
  {
    X = x;
    Y = y;
    Z = z;
  }
  
  public SVector3(Vector3 vector)
  {
    X = vector.X;
    Y = vector.Y;
    Z = vector.Z;
  }
  public Vector3 ToVector3()
  {
    return new Vector3(X, Y, Z);
  }

  

}