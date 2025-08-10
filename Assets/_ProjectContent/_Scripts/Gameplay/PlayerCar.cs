using System;
using Gameplay.Path;
using Infrastructure.Factories;
using UnityEngine;

namespace Gameplay
{
  public class PlayerCar : MonoBehaviour
  {
    public Action OnDeath { get; set; } 
    public new Rigidbody rigidbody;
    public PathRecorder pathRecorder;
    
    private IGameLoopStateMachineFactory _gameLoopStateMachineFactory;
    private bool _isDeath;

    private void Update()
    {
      if (transform.position.y <= -20 && !_isDeath) 
        Death();
    }

    public void SetZeroVelocity()
    {
      rigidbody.linearVelocity = Vector3.zero;
    }
    
    public async void Death()
    {
      pathRecorder.ResetPath();
      OnDeath?.Invoke();
    }
  }
}