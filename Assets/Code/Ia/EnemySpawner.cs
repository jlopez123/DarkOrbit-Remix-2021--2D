using System;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private MapSpawnConfiguration _mapSpawnConfiguration;
    [SerializeField]
    private Vector2 _maxRandomPosition;
    [SerializeField]
    private int _maxNpcs = 10;
    [SerializeField]
    private bool _spawnAllOnStart = false;
    [SerializeField]
    private float _startDelay = 1f;

    private ShipFactory _shipFactory;
    private async void Start()
    {
        _shipFactory = ServiceLocator.Instance.GetService<ShipFactory>();

        if (_spawnAllOnStart == false)
            return;

        await Task.Delay(TimeSpan.FromSeconds(_startDelay));

        for (int i = 0; i < _maxNpcs; i++)
        {
            var shipConfiguration = _mapSpawnConfiguration.SpawnConfiguration[UnityEngine.Random.Range(0, _mapSpawnConfiguration.SpawnConfiguration.Length)];
            SpawnNPC(shipConfiguration, GetRandomPostion(), Quaternion.identity);
        }
    }

    private void SpawnNPC(ShipBaseConfiguration shipConfiguration, Vector3 position, Quaternion rotation)
    {
        var ship = _shipFactory.Create(shipConfiguration.ShipId.Value).WithBaseConfiguration(shipConfiguration).
            WithPostion(position).WithRotation(rotation).
            WithInputMode(ShipBuilder.InputMode.Ia).WithTeam(Team.Aliens).WithTargetInfo(new TargetInfo(shipConfiguration.ShipId.ShipName, "", 0, Company.Default))
            .Build();

        ship.Health.OnDied += health => Npc_OnDied(ship,health);
    }

    private void Npc_OnDied(IShip ship,IHealth health)
    {
        var destroyer = health.LastDamager.Owner;

        if (destroyer.TargetInfo.Team == Team.Aliens)
            return;
        //verificar si el Local Player

        ServiceLocator.Instance.GetService<IEventQueue>().EnqueueEvent(new NpcDestroyedEventData(ship));
    }

    private Vector3 GetRandomPostion()
    {
        return new Vector3(UnityEngine.Random.Range(-_maxRandomPosition.x, _maxRandomPosition.x), UnityEngine.Random.Range(-_maxRandomPosition.y, _maxRandomPosition.y));
    }
}
