using System;
using UnityEngine;


// ship, structures, bonus boxes , etc
// SHIP : Company - Type(alien,player)
// Structure : Company
// boxes : ....
public interface ITargetable
{
    string Id { get; }
    TargetInfo TargetInfo { get;  }
    GameObject gameObject { get; }
    Transform transform { get;  }
    IHealth Health { get; }
    bool CanBeTargetable { get; }


}
