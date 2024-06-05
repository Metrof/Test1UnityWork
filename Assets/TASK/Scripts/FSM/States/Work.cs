using AxGrid.FSM;
using AxGrid;
using AxGrid.Model;

[State("WorkState")]
public class Work : FSMState
{
    private int _salary;
    public Work(int salary)
    {
        _salary = salary;
    }
    [Enter]
    private void Enter()
    {
        Model?.EventManager.Invoke($"OnBtnEnableChanged");
        Model.EventManager.Invoke($"Start{Constants.ButtonKeys[BildingTypes.WorkBilding]}Anim");
    }

    [Bind("OnBtn")]
    private void ChangeState(string eventStringArgs)
    {
        if (eventStringArgs == Constants.ButtonKeys[BildingTypes.WorkBilding])
        {
            return;
        }
        Parent.Change("MovingState");

        Settings.Fsm?.Invoke("OnStartMove", eventStringArgs);
    }

    [Loop(1f)]
    private void MineMoney()
    {
        Model.Inc(Constants.MoneyFieldName, _salary);
        Model.EventManager.Invoke($"On{Constants.MoneyFieldName}Changed");
        Model.EventManager.Invoke("StartMoneyAnim", _salary);
    }

    [Exit]
    private void Exit()
    {
        Model.EventManager.Invoke($"Stop{Constants.ButtonKeys[BildingTypes.WorkBilding]}Anim");
    }
}
