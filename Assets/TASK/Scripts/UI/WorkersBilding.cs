using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

public class WorkersBilding : MonoBehaviourExt
{
    [SerializeField] private BildingTypes _type;
    [SerializeField] private float _shakeSpeed = 1;
    [SerializeField] private float _shakeForce = 1.3f;

    [OnAwake]
    private void SubscribeEvents()
    {
        Model.EventManager.AddAction($"Start{Constants.ButtonKeys[_type]}Anim", StartPlayBildingAnim);
        Model.EventManager.AddAction($"Stop{Constants.ButtonKeys[_type]}Anim", StopPlayBildingAnim);
    }
    [OnStart]
    private void start()
    {
        switch (_type)
        {
            case BildingTypes.HomeBilding:
                Model.Set(Constants.ButtonKeys[BildingTypes.HomeBilding], (Vector2)GetComponent<RectTransform>().position);
                break;
            case BildingTypes.WorkBilding:
                Model.Set(Constants.ButtonKeys[BildingTypes.WorkBilding], (Vector2)GetComponent<RectTransform>().position);
                break;
            case BildingTypes.ShopBilding:
                Model.Set(Constants.ButtonKeys[BildingTypes.ShopBilding], (Vector2)GetComponent<RectTransform>().position);
                break;
            default:
                break;
        }
        Model.Set(Constants.MoneyFieldName, 0);
    }
    private void StartPlayBildingAnim()
    {
        Vector2 startScale = transform.localScale;
        Vector2 endScale = startScale * _shakeForce;

        Path.EasingLinear(_shakeSpeed, 0, 1, f =>
        {
            transform.localScale = Vector2.LerpUnclamped(startScale, endScale, f);
        }).
        EasingLinear(_shakeSpeed, 0, 1, f =>
        {
            transform.localScale = Vector2.LerpUnclamped(endScale, startScale, f);
        });
        Path.Loop = true;
    }
    private void StopPlayBildingAnim()
    {
        Path.StopPath();
        Path.Loop = false;
    }

    [OnDestroy]
    private void RemoveAction()
    {
        Model.EventManager.RemoveAction($"Start{Constants.ButtonKeys[_type]}Anim", StartPlayBildingAnim);
        Model.EventManager.RemoveAction($"Stop{Constants.ButtonKeys[_type]}Anim", StopPlayBildingAnim);
    }
}
