public class NpcDestroyedEventData : EventData
{
    public readonly ITargetable Ship;
    public NpcDestroyedEventData(ITargetable ship) : base (EventIds.NpcDestroyed)
    {
        Ship = ship;
    }
}