using System;

namespace Deadspell.Core
{
    [Flags]
    public enum VendorCategory
    {
        None,
        Junk = 1 << 1,
        Weapon = 1 << 2,
        Food = 1 << 3,
        Clothes = 1 << 4,
        Armor = 1 << 5,
        Alchemy = 1 << 6,
    }
}