using UnityEngine;

[CreateAssetMenu(fileName = "ShipId", menuName = "Create Ship Id", order = 0)]
public class ShipId : ScriptableObject
{
    [SerializeField]
    private string _value;
    [SerializeField]
    private string _shipName;
    public string Value => _value;
    public string ShipName => _shipName;
}