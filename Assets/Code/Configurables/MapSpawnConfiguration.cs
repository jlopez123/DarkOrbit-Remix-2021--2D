using UnityEngine;

[CreateAssetMenu(fileName = "Map Configuration", menuName = "Map Spawn Configuration")]
public class MapSpawnConfiguration : ScriptableObject
{
    [SerializeField]
    private ShipBaseConfiguration[] _spawnConfiguration;

    public ShipBaseConfiguration[] SpawnConfiguration => _spawnConfiguration;
}
