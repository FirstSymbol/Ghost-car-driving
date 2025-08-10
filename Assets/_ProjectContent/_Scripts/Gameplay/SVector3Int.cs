using System;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

[Serializable]
public struct SVector3Int
{
  public int X;
  public int Y;
  public int Z;
  public int Sum => X + Y + Z;
  public SVector3Int(int x, int y, int z)
  {
    X = x;
    Y = y;
    Z = z;
  }
  
  public SVector3Int(Vector3Int vector)
  {
    X = vector.x;
    Y = vector.y;
    Z = vector.z;
  }
  public Vector3Int ToVector3Int()
  {
    return new Vector3Int(X, Y, Z);
  }
  
}