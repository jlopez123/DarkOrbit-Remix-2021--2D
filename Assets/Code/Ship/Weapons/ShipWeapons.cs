using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipWeapons : MonoBehaviour, IWeaponsController
{
    [SerializeField]
    private Transform _weaponsRoot;

    private IShip _myShip;
    private bool _weaponsOn = true;
    private WeaponsModule[] _weapons;
    private WeaponsModule _mainModule;
    public Quaternion WeaponsDirection => _weaponsRoot.rotation;

    public ITargetable CurrentTarget => _myShip.CurrentTarget;

    public IShip Owner => _myShip;

    public Transform Transform => _weaponsRoot;

    public WeaponsModule MainModule => _mainModule;

    public bool IsAttacking {get; private set; }

    private void Awake()
    {
        _weapons = GetComponentsInChildren<WeaponsModule>();
        IsAttacking = false;
    }
    public void Configure(IShip ship, int mainDamage, HashSet<WeaponType> weaponsAllowed, ProjectileId mainProjectileId, float mainWeaponsRange
        , bool multipleShootPoints)
    {
        _myShip = ship;

        foreach (var weaponModule in _weapons)
           weaponModule.Configure(this, weaponsAllowed.Contains(weaponModule.WeaponType));

        _mainModule = _weapons.FirstOrDefault(t => t.WeaponType == WeaponType.Main);
        _mainModule.SetProjectile(mainProjectileId);
        _mainModule.SetParameters(mainDamage, mainWeaponsRange, 0f, multipleShootPoints);
    }

    public void TurnWeapons(float angle)
    {
        _weaponsRoot.rotation = Quaternion.AngleAxis(-angle.GetRelativeAngle(), Vector3.forward);
    }

    public void StartAttack()
    {
        if (CanAttack() == false)
            return;

        IsAttacking = true;
        foreach (var weaponModule in _weapons)
        {
            if(weaponModule.IsEnabled)
                weaponModule.StartAttack();
        }
    }
    public void StopAttack()
    {
        IsAttacking = false;
        foreach (var weaponModule in _weapons)
        {
            weaponModule.StopAttack();
        }
    }
    private bool CanAttack()
    {
        return _weaponsOn;
    }

    public float DistanceFromTarget() => Mathf.Abs(Vector2.Distance(CurrentTarget.transform.position, transform.position));
}
