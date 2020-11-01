namespace UnderwaterGame.Items
{
    using System.Collections.Generic;
    using UnderwaterGame.Items.Armours.Chests;
    using UnderwaterGame.Items.Armours.Feet;
    using UnderwaterGame.Items.Armours.Heads;
    using UnderwaterGame.Items.Armours.Legs;
    using UnderwaterGame.Items.Edibles.Health;
    using UnderwaterGame.Items.Edibles.Magic;
    using UnderwaterGame.Items.Weapons.Magic.Wands;
    using UnderwaterGame.Items.Weapons.Melee.Swords;
    using UnderwaterGame.Items.Weapons.Melee.Tridents;
    using UnderwaterGame.Items.Weapons.Ranged.Bows;
    using UnderwaterGame.Items.Weapons.Ranged.Throwables;

    public abstract partial class Item
    {
        public static List<Item> items = new List<Item>();

        public static WoodenTrident woodenTrident;

        public static WoodenBow woodenBow;

        public static CrabGrenade crabGrenade;

        public static WoodenSword woodenSword;

        public static WoodenHelmet steelHelmet;

        public static WoodenChestplate steelChestplate;

        public static WoodenLeggings steelLeggings;

        public static WoodenBoots steelBoots;

        public static CrabShuriken crabShuriken;

        public static HealthKelp healthKelp;

        public static WoodenWand woodenWand;

        public static MagicKelp magicKelp;
        
        public static void LoadAll()
        {
            woodenTrident = Load<WoodenTrident>(1);
            woodenBow = Load<WoodenBow>(2);
            crabGrenade = Load<CrabGrenade>(3);
            woodenSword = Load<WoodenSword>(4);
            steelHelmet = Load<WoodenHelmet>(5);
            steelChestplate = Load<WoodenChestplate>(6);
            steelLeggings = Load<WoodenLeggings>(7);
            steelBoots = Load<WoodenBoots>(8);
            crabShuriken = Load<CrabShuriken>(9);
            healthKelp = Load<HealthKelp>(10);
            woodenWand = Load<WoodenWand>(11);
            magicKelp = Load<MagicKelp>(12);
        }
    }
}