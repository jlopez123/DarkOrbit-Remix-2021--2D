using System;
using System.Collections;
using UnityEngine;

public class MissileModule : WeaponsModule
{
    // -----
    [SerializeField]
    private float _reloadCooldown = 5f;

    private IEnumerator _attackCoroutine;
    private bool _isRunningCoroutine = false;

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
            if(WeaponsOnRange())
                LaunchRocket();

            yield return new WaitForSeconds(_reloadCooldown);
        }
    }
    private void LaunchRocket()
    {
        var config  = new ProjectileConfig(_weaponsController.CurrentTarget, 0 , _weaponsController.WeaponsDirection.eulerAngles.z,
            (ITargetable)_weaponsController.Owner);

        var missile = SpawnProjectile(_projectileId.Value, transform.position, _weaponsController.WeaponsDirection, config, true);
    }
    public override void SetParameters(int damage, float range, float reloadTime, bool multipleShootPoints)
    {
        throw new NotImplementedException();
    }
}
