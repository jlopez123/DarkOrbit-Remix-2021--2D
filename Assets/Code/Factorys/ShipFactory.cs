public class ShipFactory
{
    private readonly ShipImpl _baseShipPrefab;

    public ShipFactory(ShipImpl basePrefab)
    {
        _baseShipPrefab = basePrefab;
    }

    public ShipBuilder Create(string shipId)
    {
        var shipBuilder = new ShipBuilder().FromPrefab(_baseShipPrefab);

        return shipBuilder;
    }

}
