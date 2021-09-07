using System;
using UnityEngine;

public class ProjectileParticles : MonoBehaviour
{
    [SerializeField]
    private PooledMonoBehaviour _impactParticles;
    [SerializeField]
    private PooledMonoBehaviour _shieldImpactParticles;
    [SerializeField]
    private bool _randomOffset = false;
    [SerializeField]
    private bool _canHitOnShield = false;
    [SerializeField]
    private Vector4 _randomValues;
    private bool _hitOnShield;

    private float _weaponsAngle;
    private void OnEnable()
    {
        _hitOnShield = false;
    }
    public void ShowParticles(ITargetable target, float weaponsAngle)
    {
        _weaponsAngle = weaponsAngle;
        var prefab = GetPrefab(target);
        var position = GetPosition(target);
        var rotation = GetRotation(target, weaponsAngle);
        prefab.Get<PooledMonoBehaviour>(position, rotation, target.transform);
    }
    private PooledMonoBehaviour GetPrefab(ITargetable target)
    {
        if (_canHitOnShield)
        {
            if (target.Health.IsAlive && target.Health.Shield > 0)
            {
                _hitOnShield = true;
                return _shieldImpactParticles;
            }

        }
        return _impactParticles;
    }
    private Vector2 GetPosition(ITargetable target)
    {
        if(_hitOnShield)
        {
            return target.transform.position + ((Quaternion.AngleAxis(_weaponsAngle, Vector3.forward) * Vector3.right) * .8f);
        }
        return target.transform.position + GetOffset();
    }

    private Vector3 GetOffset()
    {
        if (_randomOffset == false)
            return Vector2.zero;

        return new Vector2(UnityEngine.Random.Range(_randomValues.x, _randomValues.y), 
            UnityEngine.Random.Range(_randomValues.z, _randomValues.w));
    }

    private Quaternion GetRotation(ITargetable target, float weaponsAngle)
    {
        if(_hitOnShield)
        {
            return Quaternion.AngleAxis(weaponsAngle, Vector3.forward);
            //return Quaternion.AngleAxis(transform.eulerAngles.z,  Vector3.forward);
        }
        return Quaternion.identity;
    }
}