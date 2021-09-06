using UnityEngine;

public interface IWeaponsController
{
    IShip Owner { get; }
    Quaternion WeaponsDirection { get; }
    ITargetable CurrentTarget { get;  }
    float DistanceFromTarget();
    WeaponsModule MainModule { get; }

    bool IsAttacking { get;  }

    void StartAttack();
    void StopAttack();
}
