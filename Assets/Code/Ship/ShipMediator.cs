using System;
using UnityEngine;

public class ShipMediator : MonoBehaviour, IShip
{
    private IShipInput _shipInput;
    private ShipMovement _shipMovement;
    private IShipVisual _shipSprites;
    private ShipWeapons _shipWeapons;
    private ShipHealth _shipHealth;

    private Vector3 _lastDesiredPos;
    private ITargetable _currentTarget;

    private bool _isAttacking = false;

    public bool CanBeTargetable => true;

    public IHealth Health => _shipHealth;

    public ITargetable CurrentTarget => _currentTarget;
    public Team Team { get; private set; }

    public Transform RotatedTransform => _shipWeapons.Transform;

    public TargetInfo TargetInfo {get; private set; }

    public float CurrentSpeed => _shipMovement.Speed;

    public IWeaponsController Weapons => _shipWeapons;

    public event Action<ITargetable> OnCurrentTargetChanged = delegate { };
    public event Action OnQuitTarget = delegate { };
    public void Configure(ShipConfig shipConfig)
    {
        _shipInput = shipConfig.Input;
        _shipSprites = shipConfig.ShipSprites;

        _shipHealth.Configure(this, shipConfig.HullPoints, shipConfig.ShieldPoints);
        _shipMovement.Configure(this, shipConfig.Speed);
        _shipWeapons.Configure(this, shipConfig.MainDamage, shipConfig.WeaponsAllowed, shipConfig.MainProjectile, shipConfig.MainWeaponsRange, shipConfig.MultipleShootPoints);

        Team = shipConfig.Team;
        TargetInfo = shipConfig.TargetInfo;
    }
    private void Awake()
    {
        _shipMovement = GetComponent<ShipMovement>();
        _shipWeapons = GetComponent<ShipWeapons>();
        _shipHealth = GetComponent<ShipHealth>();
    }
    private void Start()
    {
        ServiceLocator.Instance.GetService<UIShipsHudController>().OnShipAdded(this);
        _shipHealth.OnDied += Ship_OnDied;
    }
    private void Update()
    {
        if (_shipInput == null)
            return;

        DoMove();

        if (_currentTarget != _shipInput.GetTarget())
            TrySetTarget(_shipInput.GetTarget());

        if (_isAttacking)
            LookAtTargetPosition(_currentTarget.gameObject.transform.position);
        else if (_shipInput.HasNewTargetPosition())
            LookAtTargetPosition(_lastDesiredPos);


        if (_shipInput.LaserAttackPressed())
            TryAttack();

        if (_shipInput.CancelAttack())
            CancelAttack();
    }
    private void DoMove()
    {
        _lastDesiredPos = _shipInput.PositionToMove();
        _shipMovement.Move(_lastDesiredPos);
    }
    private void LookAtTargetPosition(Vector3 targetPosition)
    {
        if ((Vector2)targetPosition == (Vector2)transform.position)
            return;

        var angle = Utils.AngleBetweenVectors(targetPosition, transform.position);
        _shipSprites.UpdateSprite(angle);
        _shipWeapons.TurnWeapons(angle);
    }
    private bool CanAttackTarget()
    {
        if (_currentTarget == null) 
            return false;
        if (_currentTarget.Health.IsAlive == false)
            return false;

        return true;
    }
    private void TryAttack()
    {
        if (CanAttackTarget() == false)
            return;

        LookAtTargetPosition(_currentTarget.gameObject.transform.position);
        _shipWeapons.StartAttack();
        _isAttacking = true;
    }
    private void CancelAttack()
    {
        LookAtTargetPosition(_lastDesiredPos);
        _shipWeapons.StopAttack();
        _isAttacking = false;
    }
    private bool IsValidTarget(ITargetable target)
    {        
        return  target != null && target.CanBeTargetable && target.Health.IsAlive && target != (ITargetable)this;
    }
    private void TrySetTarget(ITargetable target)
    {
        if (IsValidTarget(target) == false)
            return;

        _currentTarget = target;
        _currentTarget.Health.OnDied += CurrentTarget_OnDead;
        OnCurrentTargetChanged(_currentTarget);
        CancelAttack();
    }

    private void CurrentTarget_OnDead(IHealth target)
    {
        target.OnDied -= CurrentTarget_OnDead;
        _currentTarget = null;
        CancelAttack();
        OnQuitTarget();
    }
    private void Ship_OnDied(IHealth health)
    {
        DisableShip();
        //Destroy(this.gameObject, 2f);
    }

    private void DisableShip()
    {
        this.gameObject.CleanPooledObjects();
        _shipSprites.Hide();
        _shipWeapons.StopAttack();
        GetComponent<BoxCollider>().enabled = false;
        this.enabled = false;
    }
}

