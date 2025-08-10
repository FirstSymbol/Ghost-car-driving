using _ProjectContent._Scripts.Gameplay.Path;
using UnityEngine;
using Zenject;

namespace _ProjectContent._Scripts.Gameplay
{
  public class Ghost : MonoBehaviour
  {
    private IPathService _pathService;

    [Inject]
    private void Inject(IPathService pathService)
    {
      _pathService = pathService;
    }

    private void Update()
    {
      if (transform.position.y <= -20) 
        Destroy(gameObject);
    }
  }
}