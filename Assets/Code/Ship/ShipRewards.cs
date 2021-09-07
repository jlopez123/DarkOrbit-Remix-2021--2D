using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipRewards", menuName = "Create ShipRewards ", order = 1)]

public class ShipRewards : ScriptableObject
{
    [SerializeField]
    private ShipId _shipId;
    [SerializeField]
    private int _credits;
    [SerializeField]
    private int _uridium;
    [SerializeField]
    private int _exp;
    [SerializeField]
    private int _honor;

    public string ShipId => _shipId.Value;
    public int Credits => _credits;
    public int Uridium => _uridium;
    public int Exp => _exp;
    public int Honor => _honor;
}
