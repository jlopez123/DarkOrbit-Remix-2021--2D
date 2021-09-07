using System.Collections;
using UnityEngine;

public class LaserWeapons : WeaponsModule
{
    [SerializeField]
    private Transform[] _shootPoints;

    private int _damage;
    private IEnumerator _attackCoroutine;
    private bool _isRunningCoroutine = false;

    private bool _multipleShootPoints;

    public override void StartAttack()
    {
        if (_isRunningCoroutine)
            return;

        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);

        _attackCoroutine = AttackCoroutine();
        StartCoroutine(_attackCoroutine);
    }

    public override void StopAttack()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);

        _isRunningCoroutine = false;
    }

    // usar timers para cooldown
    private IEnumerator AttackCoroutine()
    {
        _isRunningCoroutine = true;
        while (true)
        {
            if (WeaponsOnRange())
            {
                // recorrer array de posiciones luego ...
                if (_multipleShootPoints)
                {
                    SpawnLaser(_shootPoints[0], _damage);
                    SpawnLaser(_shootPoints[1]);
                    yield return new WaitForSeconds(0.2f);
                    SpawnLaser(_shootPoints[2]);
                    SpawnLaser(_shootPoints[3]);
                    yield return new WaitForSeconds(0.2f);
                    SpawnLaser(_shootPoints[4]);
                    yield return new WaitForSeconds(0.2f);
                    SpawnLaser(_shootPoints[2]);
                    SpawnLaser(_shootPoints[3]);
                    yield return new WaitForSeconds(0.4f);
                }
                else
                {
                    SpawnLaser(transform, _damage);
                    yield return new WaitForSeconds(1f);
                }

            }
            else
                yield return null;
        }
    }
    private void SpawnLaser(Transform point, int damage = 0)
    {
        var config = new ProjectileConfig(_weaponsController.CurrentTarget, damage, _weaponsController.WeaponsDirection.eulerAngles.z,
            (ITargetable)_weaponsController.Owner);

        var laser = SpawnProjectile(_projectileId.Value, point.position, _weaponsController.WeaponsDirection, config, false);
    }
    public override void SetParameters(int damage, float range, float reloadTime, bool multipleShootPoints)
    {
        _damage = damage;
        _maxRange = range;
        // enviar array de posiciones luego
        _multipleShootPoints = multipleShootPoints;
        //        
    }
}
