using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileId", menuName = "Factory/Create Projectile Id", order = 0)]
public class ProjectileId : ScriptableObject
{
    [SerializeField]
    private string _value;

    public string Value => _value;
}
