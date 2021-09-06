using System.Threading.Tasks;
using UnityEngine;

public class PlayerShipInstaller : MonoBehaviour
{
    [SerializeField]
    private FollowToTarget _followCam;
    [SerializeField]
    private Reticle _reticle;
    [SerializeField]
    private ShipBaseConfiguration _playerShipConfig;
    //
    [SerializeField]
    private PlayerData _player;

    private ShipBuilder _shipBuilder;
    private ShipMediator _playerShip;

    private ShipFactory _shipFactory;
    private void Awake()
    {
        ServiceLocator.Instance.GetService<UIShipsHudController>().UpdateLocalPlayerCompany(_player.Company);
    }
    private async void Start()
    {
        _shipFactory = ServiceLocator.Instance.GetService<ShipFactory>();

        SpawnPlayerShip();

        _playerShip.OnCurrentTargetChanged += PlayerShip_OnCurrentTargetChanged;
        _playerShip.OnQuitTarget += PlayerShip_OnQuitTarget;
        _followCam.SetTarget(_playerShip.transform);


        await Task.Yield();
        ServiceLocator.Instance.GetService<IEventQueue>().EnqueueEvent(new PlayerShipSpawnedEventData(_playerShip));
        ServiceLocator.Instance.GetService<UIShipsHudController>().ShowStuffFromShip(_playerShip);
    }

    private void SpawnPlayerShip()
    {
        _shipBuilder = _shipFactory.Create(_playerShipConfig.ShipId.Value).WithInputMode(ShipBuilder.InputMode.Unity).WithBaseConfiguration(_playerShipConfig);

        var playerInfo = new TargetInfo(_player.Nickname, _player.Title, _player.Rank, _player.Company);
        _playerShip = _shipBuilder.WithTeam(Team.Players).WithTargetInfo(playerInfo)
        .Build();
    }

    private void PlayerShip_OnQuitTarget()
    {
        _reticle.QuitTarget();

    }
    private void PlayerShip_OnCurrentTargetChanged(ITargetable newTarget)
    {
        newTarget.Health.OnDied += CurrentTarget_OnDied;
        _reticle.SetTarget(newTarget);
        ServiceLocator.Instance.GetService<UIShipsHudController>().ShowCurrentTargetStuff(newTarget);
    }

    private void CurrentTarget_OnDied(IHealth obj)
    {

    }
}
