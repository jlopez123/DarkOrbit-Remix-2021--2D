using System;
using TMPro;
using UnityEngine;

public class UIShipInGame : TargetableInGameUI
{

    [SerializeField]
    private TargetableInfoInMapUI _targetInfoUI;
    [SerializeField]
    private RectTransform _rectTransform;
    [SerializeField]
    private Canvas _canvas;

    private Camera _camera;
    private UIShipsHudController _shipsHudController;
    public void Configure(ITargetable ship, UIShipsHudController shipsHudController, bool isEnemy)
    {
        _targetable = ship;
        _shipsHudController = shipsHudController;

        _targetInfoUI.Configure(_targetable, isEnemy, shipsHudController.ExtraConfigurationsService);
        _healthBar.Configure(ship.Health, this);
    }
    public override void Disable()
    {
        _shipsHudController.OnShipRemoved(_targetable);
        gameObject.SetActive(false);
    }
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }
    private void LateUpdate()
    {
      //  _camera.ResetWorldToCameraMatrix();

        RectTransform parent = (RectTransform)_rectTransform.parent;

        var viewportPoint = _camera.WorldToViewportPoint(_targetable.transform.position);

        var screenPoint = _canvas.worldCamera.ViewportToScreenPoint(viewportPoint);

        Vector3 worldPoint;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(parent, screenPoint, _canvas.worldCamera, out worldPoint);
        _rectTransform.position = worldPoint;
    }
    public void ShowStuff()
    {
        _healthBar.SetVisible(true);
    }
    public void HideStuff()
    {
        _healthBar.SetVisible(false);
    }

}
