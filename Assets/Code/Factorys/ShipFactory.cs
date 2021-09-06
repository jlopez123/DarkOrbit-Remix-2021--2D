public class ShipFactory
{
    private readonly ShipMediator _baseShipPrefab;

    public ShipFactory(ShipMediator basePrefab)
    {
        _baseShipPrefab = basePrefab;
    }

    public ShipBuilder Create(string shipId)
    {
        var shipBuilder = new ShipBuilder().FromPrefab(_baseShipPrefab);

        return shipBuilder;
    }

}
