using UnityEngine;

[CreateAssetMenu(fileName = "ShipBaseConfiguration", menuName = "Create Ship Base Configuration", order = 1)]
public class ShipBaseConfiguration : ScriptableObject
{
    [SerializeField]
    private string _defaultName;
    [SerializeField]
    private ShipId _shipId;
    [SerializeField]
    private int _hullPoints;
    [SerializeField]
    private int _shieldPoints;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private ProjectileId _mainProjectile;
    [SerializeField]
    private int _mainDamage;
    [SerializeField]
    private float _mainWeaponsRange;
    // enviar array con posiciones luego ...
    [SerializeField]
    private bool _multipleShootPoints = true;
    [SerializeField]
    private bool _hasMissileModule;
    [SerializeField]
    private bool _hasRocketModule;
    [Tooltip("Solo si es NPC")]
    [SerializeField]
    private bool _aggressive;
    [SerializeField]
    private ShipAnimConfig _shipAnimConfig;

    public string DefaultName => _defaultName;
    public ShipId ShipId => _shipId;
    public int HullPoints => _hullPoints;
    public int ShieldPoints => _shieldPoints;
    public float Speed => _speed;
    public ProjectileId MainProjectile => _mainProjectile;
    public int MainDamage => _mainDamage;
    public bool MultipleShootPoints => _multipleShootPoints;
    public bool HasMissileModule => _hasMissileModule;
    public bool HasRocketModule => _hasRocketModule;
    public bool Aggressive => _aggressive;
    public ShipAnimConfig ShipAnimConfig => _shipAnimConfig;
   // public ShipRewards ShipRewards => _shipRewards;
    public float MainWeaponsRange => _mainWeaponsRange;
}