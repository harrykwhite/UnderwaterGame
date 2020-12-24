namespace UnderwaterGame.Items
{
    using System.Collections.Generic;
    using UnderwaterGame.Items.Armours.Chests;
    using UnderwaterGame.Items.Armours.Feet;
    using UnderwaterGame.Items.Armours.Heads;
    using UnderwaterGame.Items.Armours.Legs;
    using UnderwaterGame.Items.Edibles.Health;
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

        public static StoneHelmet steelHelmet;

        public static StoneChestplate steelChestplate;

        public static StoneLeggings steelLeggings;

        public static StoneBoots steelBoots;

        public static CrabShuriken crabShuriken;

        public static HealthKelp healthKelp;

        public static PinkJelly pinkJelly;

        public static PurpleJelly purpleJelly;

        public static void LoadAll()
        {
            woodenTrident = Load<WoodenTrident>(1);
            woodenBow = Load<WoodenBow>(2);
            crabGrenade = Load<CrabGrenade>(3);
            woodenSword = Load<WoodenSword>(4);
            steelHelmet = Load<StoneHelmet>(5);
            steelChestplate = Load<StoneChestplate>(6);
            steelLeggings = Load<StoneLeggings>(7);
            steelBoots = Load<StoneBoots>(8);
            crabShuriken = Load<CrabShuriken>(9);
            healthKelp = Load<HealthKelp>(10);
            pinkJelly = Load<PinkJelly>(11);
            purpleJelly = Load<PurpleJelly>(12);
        }
    }
}