using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileParticles))]
public abstract class Projectile : PooledMonoBehaviour, IDamage
{
    [SerializeField]
    private ProjectileId _id;
    [SerializeField]
    protected float _speed;
    [SerializeField]
    private float _maxLifetime = 1f;
    [SerializeField]
    private int _singleDamage;
    [SerializeField]
    private float _multiplierDamage = 1f;
    //
    [SerializeField]
    private AudioSource _audio;

    private ProjectileParticles _projectileParticles;
    private bool _reachedTarget;
    protected float _timer;

    protected int _damage;
    protected float _initialDistance;
    protected float _weaponsAngle;

    protected ITargetable _target;
    protected ITargetable _owner;

    public string Id => _id.Value;
    public int Damage => _damage;
    public ITargetable Owner => _owner;
    public int SingleProjectileDamage => _singleDamage; 
    public void Configure(ITargetable target, int damage, float weaponsAngle, ITargetable owner)
    {        
        _reachedTarget = false;
        _damage = (int)(damage * _multiplierDamage);
        _target = target;
        _owner = owner;
        _weaponsAngle = weaponsAngle;

        _timer = 0f;
        _initialDistance = Mathf.Abs(Vector2.Distance(_target.transform.position, transform.position));
        DoStart();
    }
    private void Awake()
    {
        _projectileParticles = GetComponent<ProjectileParticles>();
    }
    private void Update()
    {
        if (_target == null || _reachedTarget)
            return;

        CheckLifeTime();
        DoMove();

        if (HasReachedTarget())
            Impact();
    }

    private void Impact()
    {
        _reachedTarget = true;
        TryHitTarget();
        DoDestroy();
    }
    private void CheckLifeTime()
    {
        _timer += Time.deltaTime;

        if (_timer >= _maxLifetime)
            Impact();
    }

    private bool HasReachedTarget()
    {
        return transform.position == _target.transform.position;
    }
    private void TryHitTarget()
    {
        if (_damage <= 0)
            return;

        _projectileParticles.ShowParticles(_target, _weaponsAngle);

        if(_target.Health.IsAlive)
            _target.Health.TakeHit(this);
    }
    protected abstract void DoStart();
    protected abstract void DoMove();
    protected abstract void DoDestroy();
}
