using _ProjectContent._Scripts.Gameplay.Path;
using Cysharp.Threading.Tasks;
using Infrastructure.StateMachines.StateMachine;
using JetBrains.Annotations;

namespace Infrastructure.StateMachines.InitializationStateMachine.States
{
    [UsedImplicitly]
    public class InitializePathServiceState : BaseInitializationState, IEnterableState
    {
        private readonly IPathService _pathService;

        public InitializePathServiceState(InitializationStateMachine gameLoopStateMachine, IPathService saveService) : base(gameLoopStateMachine)
        {
            _pathService = saveService;
        }

        public async UniTask Enter()
        {
            _pathService.Initialize();
            await ToNextState();
        }

        private async UniTask ToNextState()
        {
            await _stateMachine.NextState();
        }
    }
}