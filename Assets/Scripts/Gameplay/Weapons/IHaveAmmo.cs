using System;

namespace Gameplay.Weapons
{
    public interface IHaveAmmo
    {
        int Amount { get; }
        event Action AmountChanged;

    }
}