using System.Collections;
using UnityEngine;

public class RocketLauncherModule : WeaponsModule
{
    // -----
    [SerializeField]
    private int _amount = 3;
    [SerializeField]
    private float _reloadCooldown = 10f;


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
    private IEnumerator AttackCoroutine()
    {
        _isRunningCoroutine = true;
        while (true)
        {
            if (WeaponsOnRange())
            {
                for (int i = 0; i < _amount; i++)
                    LaunchMissile(i);
            }

            yield return new WaitForSeconds(_reloadCooldown);
        }
    }
    private void LaunchMissile(int index)
    {
        var angle = _weaponsController.WeaponsDirection.eulerAngles.z - UnityEngine.Random.Range(135, 225);
        var direction = Quaternion.AngleAxis(angle, Vector3.forward);

        var config = new ProjectileConfig(_weaponsController.CurrentTarget, 0 , _weaponsController.WeaponsDirection.eulerAngles.z,
            (ITargetable)_weaponsController.Owner);

        var missile = SpawnProjectile(_projectileId.Value, transform.position, direction, config, true, index == 0);
    }
    public override void SetParameters(int damage, float range, float reloadTime, bool multipleShootPoints)
    {
        throw new System.NotImplementedException();
    }
}