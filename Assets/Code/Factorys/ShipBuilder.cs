using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder
{
    public enum InputMode
    {
        Unity,
        Ia
    }

    private ShipMediator _prefab;
    private Vector3 _position = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;

    private InputMode _inputMode;
    private IShipInput _shipInput;
    private IShipVisual _shipSprites;
    private Team _team;
    private ShipBaseConfiguration _spawnConfiguration;
    private string _nickName = "";
    private TargetInfo _targetInfo = null;

    public ShipBuilder FromPrefab(ShipMediator prefab)
    {
        _prefab = prefab;
        return this;
    }
    public ShipBuilder WithPostion(Vector3 position)
    {
        _position = position;
        return this;
    }
    public ShipBuilder WithRotation(Quaternion rotation)
    {
        _rotation = rotation;
        return this;
    }
    public ShipBuilder WithBaseConfiguration(ShipBaseConfiguration baseConfiguration)
    {
        _spawnConfiguration = baseConfiguration;
        return this;
    }
    public ShipBuilder WithInputMode(InputMode inputMode)
    {
        _inputMode = inputMode;
        return this;
    }
    public ShipBuilder WithTeam(Team team)
    {
        _team = team;
        return this;
    }
    public ShipBuilder WithTargetInfo(TargetInfo info)
    {
        _targetInfo = info;
        return this;
    }
    public ShipBuilder WithNickName(string nick)
    {
        _nickName = nick;
        return this;
    }
    private IShipInput GetInput(ShipMediator ship)
    {
        switch(_inputMode)
        {
            case InputMode.Unity:
                return new PlayerInputAdapter();
            case InputMode.Ia:
                return new IaShipInputAdapter(ship, new NpcBehaviourImpl(ship,_spawnConfiguration.Aggressive));
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private IShipVisual GetVisual(ShipMediator ship)    
    {
        var spriteRenderer = ship.GetComponent<SpriteRenderer>();
        var sprites = _spawnConfiguration.ShipAnimConfig.Sprites;
        var fps = _spawnConfiguration.ShipAnimConfig.Fps;
        var flipX = _spawnConfiguration.ShipAnimConfig.FlipX;

        if (_spawnConfiguration.ShipAnimConfig.Animated)
            return new SimpleAnimation(spriteRenderer, sprites, fps, flipX);
            
        return new RotatedShipSprites(spriteRenderer, sprites,flipX);
    }
    private HashSet<WeaponType> GetWeaponsAllowed(ShipMediator ship)
    {
        var weaponsAllowed = new HashSet<WeaponType>() { WeaponType.Main };

        if (_spawnConfiguration.HasMissileModule)
            weaponsAllowed.Add(WeaponType.Missile);

        if (_spawnConfiguration.HasRocketModule)
            weaponsAllowed.Add(WeaponType.RocketLauncher);

        return weaponsAllowed;
    }
    public ShipMediator Build()
    {
        var ship = UnityEngine.Object.Instantiate(_prefab, _position, _rotation);
        Debug.Log(_team);
        var shipConfig = new ShipConfig(GetInput(ship), GetVisual(ship), _spawnConfiguration.HullPoints, _spawnConfiguration.ShieldPoints,
            _spawnConfiguration.Speed,_spawnConfiguration.MainProjectile, _spawnConfiguration.MainDamage, GetWeaponsAllowed(ship), _team, _targetInfo
            ,_spawnConfiguration.MainWeaponsRange, _spawnConfiguration.MultipleShootPoints);

        ship.Configure(shipConfig);
        return ship;
    }
}
