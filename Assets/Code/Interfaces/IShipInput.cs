using System;
using System.Collections.Generic;
using UnityEngine;

public interface IShipInput
{
    ITargetable GetTarget();
    bool HasNewTargetPosition();
    Vector3  PositionToMove();
    bool LaserAttackPressed();
    bool MissileAttackPressed();
    bool CancelAttack();
}


public interface INpcBehaviour
{
    Vector3 DesiredPosition { get; }
    ITargetable CurrentTarget { get; }
    bool IsAttacking { get; }
    void OnReceiveAttack(ITargetable aggressor);
    void Tick();
}
