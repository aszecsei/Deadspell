using System;

namespace Deadspell.Core
{
    [Flags]
    public enum EquipmentSlot : byte
    {
        None = 0,
        MainHand = 1 << 1,
        OffHand = 1 << 2,
        Head = 1 << 3,
        Torso = 1 << 4,
        Legs = 1 << 5,
        Feet = 1 << 6,
        Hands = 1 << 7,
        Armor = Head | Torso | Legs | Feet | Hands,
    }
}