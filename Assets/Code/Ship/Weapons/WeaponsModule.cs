using System;
using UnityEngine;

public abstract class WeaponsModule : MonoBehaviour
{
    [SerializeField]
    protected ProjectileId _projectileId;
    [SerializeField]
    protected WeaponType _weaponType;
    [SerializeField]
    protected float _maxRange;

    private ProjectileFactory _projectileFactory;
    protected IWeaponsController _weaponsController;
    protected bool _isEnabled;
    public bool IsEnabled => _isEnabled;
    public WeaponType WeaponType => _weaponType;
    public void Configure(IWeaponsController weaponsController, bool isEnabled)
    {
        _weaponsController = weaponsController;
        _isEnabled = isEnabled;

        _projectileFactory = ServiceLocator.Instance.GetService<ProjectileFactory>();
    }
    public bool WeaponsOnRange() => _weaponsController.DistanceFromTarget() <= _maxRange;
    
    protected Projectile SpawnProjectile(string id,Vector3 position, Quaternion rotation, ProjectileConfig config, bool useSingleDamage, bool playAudio = false)
    {
        var projectile = _projectileFactory.Create(id, position, rotation, config, useSingleDamage,playAudio);
        return projectile;
    }
    public abstract void StartAttack();
    public abstract void StopAttack();
    // de config
    public abstract void SetParameters(int damage, float range, float reloadTime, bool multipleShootPoints);

    public void SetProjectile(ProjectileId projectileId)
    {
        _projectileId = projectileId;
    }
}
