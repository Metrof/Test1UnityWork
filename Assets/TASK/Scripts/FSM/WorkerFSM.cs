using AxGrid.Base;
using AxGrid.FSM;
using AxGrid;
using UnityEngine;

public class WorkerFSM : MonoBehaviourExt
{
    [SerializeField] private int _earnMoneyPerSec = 1;
    [SerializeField] private int _wastingMoneyPerSec = 1;

    [OnAwake]
    private void CreateFsm()
    {
        Settings.Fsm = new FSM();
        Settings.Fsm.Add(new House()); 
        Settings.Fsm.Add(new Moving()); 
        Settings.Fsm.Add(new Shop(_wastingMoneyPerSec)); 
        Settings.Fsm.Add(new Work(_earnMoneyPerSec));
    }

    [OnStart]
    private void StartFsm()
    {
        Settings.Fsm.Start("HouseState");
    }

    [OnUpdate]
    public void UpdateFsm()
    {
        Settings.Fsm.Update(Time.deltaTime); 
    }
}
