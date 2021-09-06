using System;
using System.Linq;
using UnityEngine;

public class NpcBehaviourImpl :  INpcBehaviour
{
    // implementar State pattern y dividir comportamientos
    private float _timer;
    private float _timeToNextPoint;

    private Vector3 _currTargetPosition;
    private bool _isAttacking;
    private bool _aggressiveMode;

    private ITargetable _currentTarget;
    private Transform _myPosition;
    private float _distanceToTarget;

    private Collider[] _results = new Collider[5];

    public Vector3 DesiredPosition => _currTargetPosition;
    public ITargetable CurrentTarget => _currentTarget;

    public bool IsAttacking => _isAttacking;

    private IShip _myShip;
    public NpcBehaviourImpl(ShipMediator myShip, bool aggressive)
    {
        _myShip = myShip;
        _myPosition = myShip.RotatedTransform;
        _aggressiveMode = aggressive;
    }

    // agregar layerMask
    private void TryFindTarget()
    {
        if (_currentTarget != null) 
            return;

        var hits = Physics.OverlapSphereNonAlloc(_myPosition.position, 5f, _results);
        for(int i = 0; i<  hits; i++)
        {
            var ship = _results[i].GetComponent<IShip>();

            if (ship == null)
                continue;
            if (ship == _myShip || ship.Team == Team.Aliens)
                return;

            if (ship.Health.IsAlive && ship.CanBeTargetable)
                SetTarget(ship);
        }        
    }
    public void OnReceiveAttack(ITargetable aggressor)
    {
        if(aggressor.Health.IsAlive)
        {
            SetTarget(aggressor);
            _isAttacking = true;
        }
    }

    private void SetTarget(ITargetable aggressor)
    {
        if(_currentTarget != aggressor)
        {
            _currentTarget = aggressor;
            _currentTarget.Health.OnDied += Health_OnDied;
        }
    }

    private void Health_OnDied(IHealth obj)
    {
    
        QuitTarget();
    }

    public void Tick()
    {
        if(_aggressiveMode)
            TryFindTarget();

        _timer += Time.deltaTime;

        GetDistanceToTarget();
        if (_aggressiveMode)
        {
            if(CurrentTargetIsOnAggressiveRange())
                _isAttacking = true;
        }
        if (_isAttacking == false)
        {
            if (TimeToGetNewTargetPosition())
                GetNewRandomPosition();

            return;
        }

        GetPosition();

        if (CurrentTargetIsOnRange() == false)
        {
            _isAttacking = false;
            _currentTarget = null;
        }

    }
    private void GetPosition()
    {
        if (CurrentTargetIsOnChaseRange())
            return;

        _currTargetPosition = _currentTarget.transform.position + (_myPosition.right * UnityEngine.Random.Range(3f, 4f)) + GetRandomOffset();
    }
    private void QuitTarget()
    {
        _isAttacking = false;
        _currentTarget = null;
    }
    private void GetDistanceToTarget()
    {
        if (_currentTarget == null)
            return;

        _distanceToTarget = Mathf.Abs(Vector2.Distance(_myPosition.position, _currentTarget.transform.position));

    }
    private bool TimeToGetNewTargetPosition() => _timer >= _timeToNextPoint;
    private void GetNewRandomPosition()
    {
        _timer = 0;
        _timeToNextPoint = UnityEngine.Random.Range(5f, 10f);
        _currTargetPosition = new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f));
    }

    // pasar de config npc ---
    private bool CurrentTargetIsOnChaseRange() => _distanceToTarget <= 5f;

    // preguntar del ship(weapons/config max range)
    private bool CurrentTargetIsOnRange() => _distanceToTarget <= 12f;

    private Vector3 GetRandomOffset() => new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));

    private bool CurrentTargetIsOnAggressiveRange() => _currentTarget != null && _distanceToTarget <= 5f;


}