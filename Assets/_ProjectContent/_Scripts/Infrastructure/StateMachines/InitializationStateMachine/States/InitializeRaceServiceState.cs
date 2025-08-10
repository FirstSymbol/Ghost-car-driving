using Gameplay.Race;
using Cysharp.Threading.Tasks;
using Infrastructure.StateMachines.StateMachine;
using JetBrains.Annotations;

namespace Infrastructure.StateMachines.InitializationStateMachine.States
{
    [UsedImplicitly]
    public class InitializeRaceServiceState : BaseInitializationState, IEnterableState
    {
        private readonly IRaceService _raceService;

        public InitializeRaceServiceState(InitializationStateMachine gameLoopStateMachine, IRaceService raceService) : base(gameLoopStateMachine)
        {
            _raceService = raceService;
        }

        public async UniTask Enter()
        {
            _raceService.Initialize();
            await ToNextState();
        }

        private async UniTask ToNextState()
        {
            await _stateMachine.NextState();
        }
    }
}