using System;
using System.Threading.Tasks;
using UnityEngine;

public class ShipHealth : MonoBehaviour, IHealth
{
    private float _maxHullPoints;

    private float _maxShieldPoints;

    public float _currentHullPoints;
    public float _currentShieldPoints;
    private IShip _myShip;

    public event Action<float, float> OnHealthChanged = delegate { };
    public event Action<IHealth> OnDied = delegate { };
    //
    public event Action<IDamage> OnHit = delegate { };

    public bool IsAlive { get; private set; }

    public float Hull => _currentHullPoints;

    public float Shield => _currentShieldPoints;

    public ITargetable LastDamager { get; private set; }
    public void Configure(IShip myShip, int maxHullPoints, int maxShieldPoints)
    {
        _myShip = myShip;
        _maxHullPoints = maxHullPoints;
        _maxShieldPoints = maxShieldPoints;
        _currentHullPoints = _maxHullPoints;
        _currentShieldPoints = _maxShieldPoints;  
    }
    private async void SendUpdateHealthAfterFrame()
    {
        await Task.Yield();
        OnHealthChanged(_currentHullPoints / _maxHullPoints, _currentShieldPoints / _maxShieldPoints);
    }

    private void Start()
    {
        IsAlive = true;
        SendUpdateHealthAfterFrame();

    }
    public void TakeHit(IDamage hitBy)
    {
        ModifyHealth(hitBy.Damage);
        LastDamager = hitBy.Owner;
        //
        OnHit(hitBy);
    }

    private void ModifyHealth(int damage)
    {
        if(_currentShieldPoints < damage)
        {
            damage -= (int)_currentShieldPoints;
            _currentShieldPoints = 0f;
            _currentHullPoints -= damage;
        }
        else
            _currentShieldPoints -= damage;

        _currentHullPoints = Mathf.Clamp(_currentHullPoints, 0, _maxHullPoints);
        _currentShieldPoints = Mathf.Clamp(_currentShieldPoints, 0, _maxShieldPoints);

        OnHealthChanged(_currentHullPoints / _maxHullPoints, _currentShieldPoints / _maxShieldPoints);

        if(_currentHullPoints <= 0f)
            Die();
    }

    private void Die()
    {
        IsAlive = false;
        OnDied(this);
    }
}
