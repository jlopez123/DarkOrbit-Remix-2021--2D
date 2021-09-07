using System;

public interface ITakeHit
{

    void TakeHit(IDamage hitBy);

    event Action<IDamage> OnHit;
}
