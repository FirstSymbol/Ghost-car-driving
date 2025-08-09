using System;
using UnityEngine;

namespace Gameplay.Observers
{
  [RequireComponent(typeof(Rigidbody2D))]
  public class CollisionObserver2D : MonoBehaviour
  {
    public Action<Collision2D> OnEnter;
    public Action<Collision2D> OnExit;

    private void OnCollisionEnter2D(Collision2D collision) => 
      OnEnter?.Invoke(collision);

    private void OnCollisionExit2D(Collision2D collision) => 
      OnExit?.Invoke(collision);
  }
}