using UnityEngine;

namespace _ProjectContent._Scripts.Gameplay.Path
{
  public class PathPlayer : MonoBehaviour
  {
    private IPathService _pathService;

    private void Inject(IPathService pathService)
    {
      _pathService = pathService;
    }
  }
}