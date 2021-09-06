using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShipsHudController : MonoBehaviour
{
    [SerializeField]
    private UIShipInGame _shipInGameUIPrefab;

    private Dictionary<ITargetable, UIShipInGame> _shipHuds = new Dictionary<ITargetable, UIShipInGame>();

    private ITargetable _currentTarget;

    private Company _localPlayerCompany;

    public ExtraConfigurationsService ExtraConfigurationsService { get; private set; }

    private void Awake()
    {
        ExtraConfigurationsService = ServiceLocator.Instance.GetService<ExtraConfigurationsService>();
    }
    public void UpdateLocalPlayerCompany(Company localPlayerCompany)
    {
        _localPlayerCompany = localPlayerCompany;
    }
    public void OnShipAdded(ITargetable ship)
    {
        var shipUI = Instantiate(_shipInGameUIPrefab, this.transform, false);
        shipUI.Configure(ship, this , ship.TargetInfo.Company != _localPlayerCompany);

        _shipHuds.Add(ship,shipUI);
        HideStuffFromShip(ship);
    }

    public void OnShipRemoved(ITargetable ship)
    {
        if (_shipHuds.ContainsKey(ship) == false)
            return;

        _shipHuds.Remove(ship);
    }
    public void ShowCurrentTargetStuff(ITargetable target)
    {
        if (_currentTarget != null)
            HideStuffFromShip(_currentTarget);

        _currentTarget = target;
        ShowStuffFromShip(target);
    }
    public void ShowStuffFromShip(ITargetable ship)
    {
        if (_shipHuds.ContainsKey(ship) == false)
            return;

        _shipHuds[ship].ShowStuff();
    }
    private void HideStuffFromShip(ITargetable ship)
    {
        if (_shipHuds.ContainsKey(ship) == false)
            return;

        _shipHuds[ship].HideStuff();
    }
}
