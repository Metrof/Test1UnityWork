using AxGrid.FSM;
using AxGrid.Model;

[State("MovingState")]
public class Moving : FSMState
{
    private string _nextStateButtonName;

    [Enter]
    public void Enter()
    {
        Model?.EventManager.Invoke("ChangeButtonsState");
    }
    [Bind("OnStartMove")]
    private void StartWorkerMove(string buttonName)
    {
        Model?.EventManager.Invoke("PlayerMove", buttonName);
        _nextStateButtonName = buttonName;
    }
    [Bind("OnStopMove")]
    private void StopWorkerMove()
    {
        switch (_nextStateButtonName)
        {
            case "Home":
                Parent.Change("HouseState");
                break;
            case "Work":
                Parent.Change("WorkState");
                break;
            case "Shop":
                Parent.Change("ShopState");
                break;
            default:
                break;
        }
    }
    [Exit]
    public void Exit()
    {
        Model?.EventManager.Invoke("ChangeButtonsState");
    }
}
