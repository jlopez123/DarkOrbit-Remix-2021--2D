using UnityEngine;

[DefaultExecutionOrder(-10)]
public class GameInstaller : GeneralInstaller
{
    [SerializeField]
    private UIShipsHudController _shipsHudController;
    [SerializeField]
    private ShipMediator _shipBasePrefab;
    [SerializeField]
    private ProjectilesConfiguration _projectilesConfiguration;
    protected override void DoInstallDependencies()
    {
        ServiceLocator.Instance.RegisterService<UIShipsHudController>(_shipsHudController);

        InstallShipFactory();
        InstallProjectileFactory();
    }
    private void OnDestroy()
    {
        ServiceLocator.Instance.UnRegisterService<UIShipsHudController>();
    }

    private void InstallShipFactory()
    {
        var shipFactory = new ShipFactory(_shipBasePrefab);
        ServiceLocator.Instance.RegisterService<ShipFactory>(shipFactory);
    }
    private void InstallProjectileFactory()
    {
        var projectileFactory = new ProjectileFactory(Instantiate(_projectilesConfiguration));
        ServiceLocator.Instance.RegisterService<ProjectileFactory>(projectileFactory);
    }

    protected override void DoStart()
    {

    }
}