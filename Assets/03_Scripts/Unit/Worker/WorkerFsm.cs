namespace FSM.Test
{
    public class WorkerFsm
    {
        // private readonly IFsmController _fsmController = new FsmController();
        //
        // private readonly GameManager _gameManager;
        //
        // private readonly WorkerUnit _workerUnit;
        //
        // public WorkerFsm(GameManager gameManager, WorkerUnit workerUnit)
        // {
        //     _gameManager = gameManager;
        //     _workerUnit = workerUnit;
        // }
        //
        // public void Start()
        // {
        //     _fsmController.AddState("idle", new IdleState(_fsmController, _gameManager, _workerUnit));
        //     _fsmController.AddState("mining", new PatrolState(_fsmController, _gameManager, _workerUnit));
        //     
        //     _fsmController.AddTransition(new Transition("idle", "mining", new BoolTrigger(true)));
        //     _fsmController.AddTransition(new Transition("mining", "idle", new BoolTrigger(false)));
        //     
        //     _fsmController.Start("mining");
        // }
        //
        // public void Update()
        // {
        //     _fsmController.Update();
        //
        //     bool hasMineral = false;
        //     foreach (IUnit unit in _gameManager.MineralList)
        //     {
        //         MineralUnit mineralUnit = (MineralUnit)unit;
        //         if (mineralUnit.Minerals > 0)
        //         {
        //             hasMineral = true;
        //             break;
        //         }
        //     }
        //     
        //     _fsmController.SendTrigger(new BoolTriggerValue(hasMineral));
        // }
    }
}