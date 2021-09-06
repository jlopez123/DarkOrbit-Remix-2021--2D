public class ProjectileConfig
{
    public ITargetable Target;
    public int Damage;
    public float Direction;
    public ITargetable Owner;

    public ProjectileConfig(ITargetable target, int damage, float direction, ITargetable owner)
    {
        Target = target;
        Damage = damage;
        Direction = direction;
        Owner = owner;
    }   

}