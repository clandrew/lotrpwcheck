using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lotrpwcheck
{
    static class Names
    {
        public static string[] AllItems = new string[]
        {
            "Tomb Key",
            "Moria Key",
            "Red Gateway Gem",
            "Elvish Book",
            "Magic Rock",
            "Bottle",
            "Lost Amulet",
            "Maggot Note",
            "Scroll Of Floi",

            "Gate Key",
            "Moria Key",
            "Yellow Gateway Gem",
            "Book Of The Ages",
            "Gold Piece",
            "Jug Of Honey",
            "Lost Amulet",
            "Old Willow Note",
            "Scroll Of Oin",

            "Tomb Key",
            "Moria Key",
            "Gateway Keystone",
            "Book Of Mazarbul",
            "Gold Pieces",
            "Eye Glasses",
            "Lost Amulet",
            "Note From Gandalf",
            "Color Scoll",

            "Tomb Key",
            "Moria Key",
            "Green Gateway Gem",
            "Bilbo Diary",
            "Gold Pieces",
            "Healing Moss",
            "Lost Amulet",
            "Letter To Elrond",
            "Keystone Scroll",

            "Tomb Key",
            "Boat Oar",
            "Purple Gateway Gem",
            "Jeweled Ring",
            "Gold Pieces",
            "Athelas Major",
            "Lost Amulet",
            "Horn Of Boromir",
            "Long Bow",

            "Key To Bree",
            "Healing Mushroom",
            "Violet Gateway Gem",
            "The Ring",
            "Athelas Minor",
            "Healing Fruit",
            "Lost Amulet",
            "Magic Fern",
            "Orb of Drexle",
        };

        public enum Weapon
        {
            OldDagger,
            Dagger,
            BarrowDagger,
            TrollDagger,
            ElvishDagger,
            Sting,
            LightSword,
            Sword,
            Invalid,
            Corruption
        }

        public static string[] AllWeapons = new string[]
        {
            "Old Dagger",
            "Dagger",
            "Barrow Dagger",
            "Troll Dagger",
            "Elvish Dagger",
            "Sting",
            "Light Sword",
            "Sword",
            "(invalid)",
            "(corruption)"
        };

        public enum Armor
        {
            ClothCloak,
            PlateMail,
            PaddedArmor,
            MithrilArmor,
            LeatherArmor,
            ChainMail,
            Invalid,
            Corruption
        }

        public static string[] AllArmor = new string[]
        {
            "Cloth Cloak",
            "Plate Mail",
            "Padded Armor",
            "Mithril Armor",
            "Leather Armor",
            "Chain Mail",
            "(invalid)",
            "(corruption)"
        };
    }
}
