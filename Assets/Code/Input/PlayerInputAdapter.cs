using UnityEngine;

public class PlayerInputAdapter : IShipInput
{
    public PlayerInputAdapter()
    {
    }
    public ITargetable GetTarget() => PlayerInputManager.Instance.CurrentTarget;
    public bool HasNewTargetPosition()
    {
        return PlayerInputManager.Instance.ClickedPosChanged;
    }
    public Vector3 PositionToMove()
    {
        return PlayerInputManager.Instance.TargetPositionToMove;
    }
    public bool LaserAttackPressed()
    {
        return PlayerInputManager.Instance.LasserAttackPressed;
    }

    public bool MissileAttackPressed()
    {
        return PlayerInputManager.Instance.MissileAttackPressed;
    }
    public bool CancelAttack()
    {
        return PlayerInputManager.Instance.CancelPrimaryAttackPressed;
    }
}
