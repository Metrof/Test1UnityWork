using AxGrid.FSM;
using AxGrid;
using AxGrid.Model;

[State("ShopState")]
public class Shop : FSMState
{
    private int _prices;
    public Shop(int prices)
    {
        _prices = prices;
    }

    [Enter]
    private void Enter()
    {
        Model.EventManager.Invoke($"Start{Constants.ButtonKeys[BildingTypes.ShopBilding]}Anim");
    }

    [Loop(1f)]
    private void MineMoney()
    {
        if (Model.GetInt(Constants.MoneyFieldName) <= 0)
        {
            ChangeState("Home");
            return;
        }
        Model.Dec(Constants.MoneyFieldName, _prices);
        Model.EventManager.Invoke($"On{Constants.MoneyFieldName}Changed");
        Model.EventManager.Invoke("StartMoneyAnim", -_prices);
    }

    [Bind("OnBtn")]
    private void ChangeState(string eventStringArgs)
    {
        if (eventStringArgs == Constants.ButtonKeys[BildingTypes.ShopBilding])
        {
            return;
        }
        Parent.Change("MovingState");

        Settings.Fsm?.Invoke("OnStartMove", eventStringArgs);
    }

    [Exit]
    private void Exit()
    {
        Model.EventManager.Invoke($"Stop{Constants.ButtonKeys[BildingTypes.ShopBilding]}Anim");
    }
}
