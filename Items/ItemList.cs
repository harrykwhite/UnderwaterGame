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

        public static WoodenSword woodenSword;

        public static StoneHelmet steelHelmet;

        public static StoneChestplate steelChestplate;

        public static StoneLeggings steelLeggings;

        public static StoneBoots steelBoots;

        public static PinkJellyShuriken pinkJellyShuriken;

        public static PurpleJellyShuriken purpleJellyShuriken;

        public static HealthKelp healthKelp;

        public static PinkJelly pinkJelly;

        public static PurpleJelly purpleJelly;

        public static void LoadAll()
        {
            woodenTrident = Load<WoodenTrident>(1);
            woodenBow = Load<WoodenBow>(2);
            woodenSword = Load<WoodenSword>(3);
            steelHelmet = Load<StoneHelmet>(4);
            steelChestplate = Load<StoneChestplate>(5);
            steelLeggings = Load<StoneLeggings>(6);
            steelBoots = Load<StoneBoots>(7);
            pinkJellyShuriken = Load<PinkJellyShuriken>(8);
            purpleJellyShuriken = Load<PurpleJellyShuriken>(9);
            healthKelp = Load<HealthKelp>(10);
            pinkJelly = Load<PinkJelly>(11);
            purpleJelly = Load<PurpleJelly>(12);
        }
    }
}