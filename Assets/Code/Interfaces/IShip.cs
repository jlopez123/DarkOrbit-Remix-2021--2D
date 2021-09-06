public interface IShip : ITargetable
{
    Team Team { get; }
    ITargetable CurrentTarget { get; }
    IWeaponsController Weapons { get; }
    float CurrentSpeed { get; }
}