using UnityEngine;

public interface IDamage
{
    ITargetable Owner { get; }
    Transform transform { get; }
    int Damage { get; }
}
