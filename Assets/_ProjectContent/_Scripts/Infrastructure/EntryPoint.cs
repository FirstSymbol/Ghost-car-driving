using System;
using Infrastructure.Factories;
using Infrastructure.StateMachines.GameLoopStateMachine.States;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
  public class EntryPoint : MonoBehaviour
  {
    private IGameLoopStateMachineFactory _stateMachineFactory;

    public static bool HasStarted { get; private set; }

    private async void Start()
    {
      HasStarted = true;
      await _stateMachineFactory.GetFrom(this).Enter<EntryPointState>();
    }

    [Inject]
    private void Inject(IGameLoopStateMachineFactory stateMachineFactory)
    {
      _stateMachineFactory = stateMachineFactory;
    }

#if UNITY_EDITOR
    [InitializeOnEnterPlayMode]
    private static void InitializeOnEnterPlayMode()
    {
      HasStarted = false;
    }    
#endif

  }
}