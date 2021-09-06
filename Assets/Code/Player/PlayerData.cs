using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Create Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string _nickname = "";
    [SerializeField]
    private string _title = "";
    [SerializeField]
    private ShipBaseConfiguration _shipConfig;
    [SerializeField]
    private Company _company;
    [SerializeField]
    private int _level;
    [SerializeField]
    private int _rank;
    [SerializeField]
    private int _exp;
    [SerializeField]
    private int _honor;
    [SerializeField]
    private int _uridium;
    [SerializeField]
    private int _credits;

    public string Nickname => _nickname;
    public ShipBaseConfiguration ShipConfig => _shipConfig;
    public Company Company => _company;
    public int Level => _level; 
    public int Rank => _rank; 
    public int Exp => _exp;
    public int Uridium => _uridium;
    public int Honor => _honor;
    public int Credits => _credits;
    public string Title => _title;
}