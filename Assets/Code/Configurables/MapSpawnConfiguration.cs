using UnityEngine;

[CreateAssetMenu(fileName = "Map Configuration", menuName = "Configurables/Map Spawn Configuration")]
public class MapSpawnConfiguration : ScriptableObject
{
    [SerializeField]
    private ShipBaseConfiguration[] _spawnConfiguration;

    public ShipBaseConfiguration[] SpawnConfiguration => _spawnConfiguration;
}
