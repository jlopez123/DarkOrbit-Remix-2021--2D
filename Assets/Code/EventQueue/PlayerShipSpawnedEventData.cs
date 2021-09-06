public class PlayerShipSpawnedEventData : EventData
{
    public readonly IShip PlayerShip;
    public PlayerShipSpawnedEventData(IShip playerShip) : base (EventIds.PlayerShipSpawned)
    {
        PlayerShip = playerShip;
    }
}
