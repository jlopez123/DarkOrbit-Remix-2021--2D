using UnityEngine;

public class IaShipInputAdapter : IShipInput
{
    private ShipImpl _myShip;
    private INpcBehaviour _npcBehaviour;
    public IaShipInputAdapter(ShipImpl ship, INpcBehaviour npcBehaviour)
    {
        _myShip = ship;
        _npcBehaviour = npcBehaviour;
        _myShip.Health.OnHit += Health_OnHit;
    }

    private void Health_OnHit(IDamage damager)
    {
        _npcBehaviour.OnReceiveAttack(damager.Owner);
    }

    public bool CancelAttack()
    {
        return _npcBehaviour.IsAttacking == false;
    }

    public ITargetable GetTarget()
    {
        _npcBehaviour.Tick();
        return _npcBehaviour.CurrentTarget;
    }

    public bool HasNewTargetPosition()
    {
        return true;
    }

    public bool LaserAttackPressed()
    {
        return _npcBehaviour.IsAttacking;
    }

    public bool MissileAttackPressed()
    {
        return false;
    }

    public Vector3 PositionToMove()
    {
        return _npcBehaviour.DesiredPosition;
    }
}
