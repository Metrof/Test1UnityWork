using AxGrid.Base;
using AxGrid;
using AxGrid.Path;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class Worker : MonoBehaviourExt
{
    [Header("AnimSettings")]
    [SerializeField] private float _timeAnim = 1.5f;

    private SpriteRenderer _spriteRenderer;
    private WorkersBilding _currentBilding;

    private List<Vector2> _pos = new List<Vector2>();
    private Dictionary<string, Vector2> _bildingPositions = new();

    [OnAwake]
    private void awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Settings.Model.EventManager.AddAction<string>("PlayerMove", StartMove);
    }

    [OnStart(RunLevel.Low)]
    private void Init()
    {
        Model.Set(Constants.MoneyFieldName, 0);
        _spriteRenderer.enabled = false;

        SetBildingPos();
    }
    private void SetBildingPos()
    {
        _bildingPositions.Add(Constants.ButtonKeys[BildingTypes.HomeBilding], 
            Model.Get<Vector2>(Constants.ButtonKeys[BildingTypes.HomeBilding]));

        _bildingPositions.Add(Constants.ButtonKeys[BildingTypes.WorkBilding], 
            Model.Get<Vector2>(Constants.ButtonKeys[BildingTypes.WorkBilding]));

        _bildingPositions.Add(Constants.ButtonKeys[BildingTypes.ShopBilding], 
            Model.Get<Vector2>(Constants.ButtonKeys[BildingTypes.ShopBilding]));

        transform.position = _bildingPositions[Constants.ButtonKeys[BildingTypes.HomeBilding]];
    }
    private void StartMove(string eventStringArgs)
    {
        if (_bildingPositions.ContainsKey(eventStringArgs))
        {
            _spriteRenderer.enabled = true;

            Vector2 startPos = transform.position;
            Vector2 endPos = _bildingPositions[eventStringArgs];
            float animDuration = _timeAnim * (Vector2.Distance(startPos, endPos) / 2);

            Path
                .EasingLinear(animDuration, 0, 1, f =>
                {
                    transform.position = Vector2.LerpUnclamped(startPos, endPos, f);
                })
                .Action(() =>
                {
                    Settings.Fsm?.Invoke("OnStopMove");
                    _spriteRenderer.enabled = false;
                });
        }
    }
    [OnDestroy]
    private void ClearActions()
    {
        Model.EventManager.RemoveAction<string>("PlayerMove", StartMove);
    }
}
