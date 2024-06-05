using AxGrid.FSM;
using AxGrid;
using AxGrid.Model;

[State("HouseState")]
public class House : FSMState
{
    [Enter]
    private void Enter()
    {
        Model.EventManager.Invoke($"Start{Constants.ButtonKeys[BildingTypes.HomeBilding]}Anim");
    }

    [Bind("OnBtn")]
    private void ChangeState(string eventStringArgs)
    {
        if (eventStringArgs == Constants.ButtonKeys[BildingTypes.HomeBilding])
        {
            return;
        }
        Parent.Change("MovingState");

        Settings.Fsm?.Invoke("OnStartMove", eventStringArgs);
    }

    [Exit]
    private void Exit()
    {
        Model.EventManager.Invoke($"Stop{Constants.ButtonKeys[BildingTypes.HomeBilding]}Anim");
    }
}
