using Gameplay.Race;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Saving;
using Infrastructure.StateMachines.StateMachine;
using Zenject;

namespace Infrastructure.StateMachines.GameLoopStateMachine.States
{
    public class GameplayState : BaseGameLoopState, IEnterableState
    {
        private readonly ISaveService _saveService;
        private readonly IWaypointsService _waypointsService;

        [Inject]
        public GameplayState(GameLoopStateMachine gameLoopStateMachine, ISaveService saveService, IWaypointsService waypointsService) : base(gameLoopStateMachine)
        {
            _saveService = saveService;
            _waypointsService = waypointsService;
        }

        public UniTask Enter()
        {
            return default;
        }

        public override UniTask Exit()
        {
            _saveService.StoreSaveFile();
            _waypointsService.WaypointsStates.Clear();
            return default;
        }
    }
}