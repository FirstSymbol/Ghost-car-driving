using System;
using Unity.Mathematics;

namespace Gameplay.Race
{
  [Serializable]
  public class RaceData
  {
    public int RaceNumber;
    public bool HasGhost;
    public SVector3Int RecordTime; 

    public RaceData(int raceNumber = 1, bool hasGhost = false, SVector3Int recordTime = new SVector3Int())
    {
      RaceNumber = raceNumber;
      HasGhost = hasGhost;
      RecordTime = recordTime;
    }
  }
}