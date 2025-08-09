using System;
using UnityEngine;

namespace Gameplay.Observers
{
  [RequireComponent(typeof(Rigidbody2D))]
  public class TriggerObserver2D : MonoBehaviour
  {
    public Action<Collider2D> OnEnter;
    public Action<Collider2D> OnExit;

    private void OnTriggerEnter2D(Collider2D collider) => 
      OnEnter?.Invoke(collider);

    private void OnTriggerExit2D(Collider2D collider) => 
      OnExit?.Invoke(collider);
  }
}