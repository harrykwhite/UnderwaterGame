using System.Collections.Generic;
using UnderwaterGame.Items.Armours.Chests;
using UnderwaterGame.Items.Armours.Feet;
using UnderwaterGame.Items.Armours.Heads;
using UnderwaterGame.Items.Armours.Legs;
using UnderwaterGame.Items.Edibles.Healing;
using UnderwaterGame.Items.Weapons.Magic.Wands;
using UnderwaterGame.Items.Weapons.Melee.Swords;
using UnderwaterGame.Items.Weapons.Melee.Tridents;
using UnderwaterGame.Items.Weapons.Ranged.Bows;
using UnderwaterGame.Items.Weapons.Ranged.Throwables;

namespace UnderwaterGame.Items
{
    public abstract partial class Item
    {
        public static List<Item> Items { get; private set; } = new List<Item>();

        public static WoodenTrident WoodenTrident { get; private set; }
        public static WoodenBow WoodenBow { get; private set; }
        public static CrabGrenade CrabGrenade { get; private set; }
        public static WoodenSword WoodenSword { get; private set; }
        public static WoodenHelmet SteelHelmet { get; private set; }
        public static WoodenChestplate SteelChestplate { get; private set; }
        public static WoodenLeggings SteelLeggings { get; private set; }
        public static WoodenBoots SteelBoots { get; private set; }
        public static CrabShuriken CrabShuriken { get; private set; }
        public static HealingKelp HealingKelp { get; private set; }
        public static WoodenWand WoodenWand { get; private set; }

        public static void LoadAll()
        {
            WoodenTrident = Load<WoodenTrident>(1);
            WoodenBow = Load<WoodenBow>(2);
            CrabGrenade = Load<CrabGrenade>(3);
            WoodenSword = Load<WoodenSword>(4);
            SteelHelmet = Load<WoodenHelmet>(5);
            SteelChestplate = Load<WoodenChestplate>(6);
            SteelLeggings = Load<WoodenLeggings>(7);
            SteelBoots = Load<WoodenBoots>(8);
            CrabShuriken = Load<CrabShuriken>(9);
            HealingKelp = Load<HealingKelp>(10);
            WoodenWand = Load<WoodenWand>(11);
        }
    }
}