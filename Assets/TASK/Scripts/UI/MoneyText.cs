using AxGrid.Base;
using AxGrid.Tools.Binders;
using TMPro;
using UnityEngine;
using AxGrid.Path;

public class MoneyText : UITextMPDataBind
{
    [Space]
    [Header("MoneyTextSettings")]
    [SerializeField] private TextMeshProUGUI _addTMP;
    [SerializeField] private float _animDuration = 0.5f;
    [SerializeField] private float _flyDistance = 70;

    private RectTransform _addTMPRect;
    private RectTransform _myRect;

    [OnAwake(RunLevel.Low)]
    private void awake()
    {
        _myRect = GetComponent<RectTransform>();
        _addTMPRect = _addTMP?.GetComponent<RectTransform>();
    }

    [OnStart(RunLevel.Low)]
    protected void start()
    {
        _addTMP?.gameObject.SetActive(false);
        Model.EventManager.AddAction<int>("StartMoneyAnim", StartMoneyAnim);
    }
    protected override void Changed()
    {
        base.Changed();
        uiText.text = Model.GetInt(Constants.MoneyFieldName).ToString();
    }
    private void StartMoneyAnim(int changeValue)
    {
        if (_addTMP != null)
        {
            _addTMP.gameObject.SetActive(true);
            _addTMP.text = changeValue > 0 ? $"+{changeValue}" : changeValue.ToString();

            _addTMPRect.localPosition = _myRect.localPosition;

            Vector3 endPos = _addTMPRect.localPosition;
            endPos.y += _flyDistance;

            Path
                    .EasingLinear(_animDuration, 0, 1, f =>
                    {
                        _addTMPRect.localPosition = Vector2.LerpUnclamped(_myRect.localPosition, endPos, f);
                    })
                    .Action(() =>
                    {
                        _addTMP?.gameObject.SetActive(false);
                    });
        }
    }

    [OnDestroy(RunLevel.Low)]
    private void RemoveAction()
    {
        Model.EventManager.RemoveAction<int>("StartMoneyAnim", StartMoneyAnim);
    }
}
