using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
  public class PlayerCarRevivier : MonoBehaviour
  {
    public PlayerCar playerCar;

    private void Start()
    {
      playerCar.OnDeath += CarDeath;
    }

    private void OnDestroy()
    {
      playerCar.OnDeath -= CarDeath;
    }

    private void CarDeath()
    {
      playerCar.SetZeroVelocity();
      playerCar.transform.DOMove(transform.position, 0f);
      playerCar.transform.DORotate(transform.rotation.eulerAngles, 0f);
    }
  }
}