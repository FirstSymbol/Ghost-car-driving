using System;

namespace Gameplay.Race
{
  [Serializable]
  public class RaceData
  {
    public int RaceNumber;
    public bool HasGhost;

    public RaceData(int raceNumber = 1, bool hasGhost = false)
    {
      RaceNumber = raceNumber;
      HasGhost = hasGhost;
    }
  }
}