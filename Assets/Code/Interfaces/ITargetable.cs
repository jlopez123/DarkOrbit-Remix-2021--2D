using System;
using UnityEngine;

public interface ITargetable
{
    TargetInfo TargetInfo { get;  }
    GameObject gameObject { get; }
    Transform transform { get;  }
    IHealth Health { get; }
    bool CanBeTargetable { get; }
}
