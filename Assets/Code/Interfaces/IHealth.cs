using System;

public interface IHealth : ITakeHit
{
    bool IsAlive { get; }

    event Action<float, float> OnHealthChanged;

    event Action<IHealth> OnDied;

    float Hull { get; }
    float Shield { get; }

}
