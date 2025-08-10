using System;

namespace _ProjectContent._Scripts.Gameplay.Race
{
  [Serializable]
  public class RaceData
  {
    public int RaceNumber;

    public RaceData(int raceNumber = 1)
    {
      RaceNumber = raceNumber;
    }
  }
}