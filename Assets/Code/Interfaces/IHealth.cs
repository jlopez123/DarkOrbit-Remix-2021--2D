using System;

public interface IHealth : ITakeHit
{
    IDamage LastDamager { get; }
    bool IsAlive { get; }

    event Action<float, float> OnHealthChanged;

    event Action<IHealth> OnDied;

    float Hull { get; }
    float Shield { get; }

}
