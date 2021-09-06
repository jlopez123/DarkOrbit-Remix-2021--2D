using System;
using UnityEngine;

[Serializable]

public class ShipRewards
{
    [SerializeField]
    private int _credits;
    [SerializeField]
    private int _uridium;
    [SerializeField]
    private int _exp;
    [SerializeField]
    private int _honor;

    public int Credits => _credits;
    public int Uridium => _uridium;
    public int Exp => _exp;
    public int Honor => _honor;
}


