using System.Collections.Generic;

public class ShipConfig
{
    public IShipInput Input;
    public IShipVisual ShipSprites;
    public int HullPoints;
    public int ShieldPoints;
    public float Speed;
    public ProjectileId MainProjectile;
    public bool MultipleShootPoints;
    public int MainDamage;
    public float MainWeaponsRange;
    public HashSet<WeaponType> WeaponsAllowed;
    public Team Team;
    public TargetInfo TargetInfo;
    public ShipConfig(IShipInput input, IShipVisual shipSprites, int hp, int sp, float speed, ProjectileId mainProjectile,
        int mDamage, HashSet<WeaponType> weaponsAllowed, Team team, TargetInfo targetInfo, float mainWeaponsRange, bool multipleShootPoints)
    {
        Input = input;
        ShipSprites = shipSprites;
        HullPoints = hp;
        ShieldPoints = sp;
        Speed = speed;
        MainProjectile = mainProjectile;
        MainDamage = mDamage;
        WeaponsAllowed = weaponsAllowed;
        Team = team;
        TargetInfo = targetInfo;
        MainWeaponsRange = mainWeaponsRange;
        MultipleShootPoints = multipleShootPoints;
    }
}