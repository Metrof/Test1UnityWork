using AxGrid.Base;
using AxGrid.Tools.Binders;
using UnityEngine;

public class WorkersButton : UIButtonDataBind
{
    [Space]
    [Header("WorkersButtonSettings")]
    [SerializeField] private BildingTypes _type;
    [SerializeField] private ParticleSystem _rings;

    [OnAwake(RunLevel.High)]
    private void SetButtonKeyName()
    {
        buttonName = Constants.ButtonKeys[_type];
    }

    [OnStart(RunLevel.Low)]
    private void SetAction()
    {
        Model.EventManager.AddAction("ChangeButtonsState", ChangeButtonState);
        Model.EventManager.AddAction($"On{buttonName}Click", PlayParticle);
    }

    private void PlayParticle()
    {
        _rings?.Play();
    }

    [OnDestroy(RunLevel.Low)]
    private void RemoveAction()
    {
        Model.EventManager.RemoveAction("ChangeButtonsState", ChangeButtonState);
        Model.EventManager.RemoveAction($"On{buttonName}Click", PlayParticle);
    }

    private void ChangeButtonState()
    {
        defaultEnable = !defaultEnable;
        OnItemEnable();
    }
}
public enum BildingTypes
{
    HomeBilding,
    WorkBilding,
    ShopBilding
}
