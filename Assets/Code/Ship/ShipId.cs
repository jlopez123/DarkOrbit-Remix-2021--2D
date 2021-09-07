using UnityEngine;

[CreateAssetMenu(fileName = "ShipId", menuName = "Create Ship Id", order = 0)]
public class ShipId : ScriptableObject
{
    [SerializeField]
    private string _value;
    public string Value => _value;
}