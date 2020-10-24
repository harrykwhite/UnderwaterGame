using System.Collections.Generic;

namespace UnderwaterGame.Sprites
{
    public partial class Sprite
    {
        public static List<Sprite> Sprites { get; private set; } = new List<Sprite>();

        public static Sprite BigSeaweed { get; private set; }
        public static Sprite SmallSeaweed { get; private set; }

        public static Sprite Jellyfish { get; private set; }
        public static Sprite TallJellyfish { get; private set; }
        public static Sprite PlayerIdle { get; private set; }
        public static Sprite PlayerSwim { get; private set; }

        public static Sprite LongSwing { get; private set; }
        public static Sprite WideSwing { get; private set; }

        public static Sprite Blood { get; private set; }
        public static Sprite Bubble0 { get; private set; }
        public static Sprite Bubble1 { get; private set; }
        public static Sprite Bubble2 { get; private set; }
        public static Sprite Smoke { get; private set; }
        public static Sprite Wood { get; private set; }
        public static Sprite CrabShell { get; private set; }
        public static Sprite Liquid { get; private set; }
        public static Sprite Fire { get; private set; }

        public static Sprite WoodenArrow { get; private set; }
        public static Sprite FlareMagic { get; private set; }

        public static Sprite WoodenTrident { get; private set; }
        public static Sprite WoodenSword { get; private set; }
        public static Sprite WoodenBow { get; private set; }
        public static Sprite CrabGrenade { get; private set; }
        public static Sprite CrabShuriken { get; private set; }
        public static Sprite WoodenWand { get; private set; }
        public static Sprite HealingKelp { get; private set; }
        public static Sprite WoodenHelmet { get; private set; }
        public static Sprite WoodenHelmetWear { get; private set; }
        public static Sprite WoodenChestplate { get; private set; }
        public static Sprite WoodenChestplateWear { get; private set; }
        public static Sprite WoodenLeggings { get; private set; }
        public static Sprite WoodenLeggingsWear { get; private set; }
        public static Sprite WoodenBoots { get; private set; }
        public static Sprite WoodenBootsWear { get; private set; }

        public static void LoadAll()
        {
            BigSeaweed = Load(Main.TextureLibrary.ENVIRONMENTALS_BIGSEAWEED);
            SmallSeaweed = Load(Main.TextureLibrary.ENVIRONMENTALS_SMALLSEAWEED);

            Jellyfish = Load(Main.TextureLibrary.CHARACTERS_ENEMIES_JELLYFISH_JELLYFISH);
            TallJellyfish = Load(Main.TextureLibrary.CHARACTERS_ENEMIES_JELLYFISH_TALLJELLYFISH);
            PlayerIdle = Load(Main.TextureLibrary.CHARACTERS_PLAYER_PLAYERIDLE);
            PlayerSwim = Load(Main.TextureLibrary.CHARACTERS_PLAYER_PLAYERSWIM);

            LongSwing = Load(Main.TextureLibrary.EFFECTS_LONGSWING);
            WideSwing = Load(Main.TextureLibrary.EFFECTS_WIDESWING);

            Blood = Load(Main.TextureLibrary.PARTICLES_BLOOD);
            Bubble0 = Load(Main.TextureLibrary.PARTICLES_BUBBLE0);
            Bubble1 = Load(Main.TextureLibrary.PARTICLES_BUBBLE1);
            Bubble2 = Load(Main.TextureLibrary.PARTICLES_BUBBLE2);
            Smoke = Load(Main.TextureLibrary.PARTICLES_SMOKE);
            Wood = Load(Main.TextureLibrary.PARTICLES_WOOD);
            CrabShell = Load(Main.TextureLibrary.PARTICLES_CRABSHELL);
            Liquid = Load(Main.TextureLibrary.PARTICLES_LIQUID);
            Fire = Load(Main.TextureLibrary.PARTICLES_FIRE);

            WoodenArrow = Load(Main.TextureLibrary.PROJECTILES_ARROWS_WOODENARROW);
            FlareMagic = Load(Main.TextureLibrary.PROJECTILES_MAGIC_FLAREMAGIC);

            WoodenTrident = Load(Main.TextureLibrary.ITEMS_WEAPONS_MELEE_TRIDENTS_WOODENTRIDENT);
            WoodenSword = Load(Main.TextureLibrary.ITEMS_WEAPONS_MELEE_SWORDS_WOODENSWORD);
            WoodenBow = Load(Main.TextureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW);
            CrabGrenade = Load(Main.TextureLibrary.ITEMS_WEAPONS_RANGED_THROWABLES_CRABGRENADE);
            CrabShuriken = Load(Main.TextureLibrary.ITEMS_WEAPONS_RANGED_THROWABLES_CRABSHURIKEN);
            WoodenWand = Load(Main.TextureLibrary.ITEMS_WEAPONS_MAGIC_WANDS_WOODENWAND);
            HealingKelp = Load(Main.TextureLibrary.ITEMS_CONSUMABLES_HEALINGKELP);
            WoodenHelmet = Load(Main.TextureLibrary.ITEMS_ARMOURS_HEADS_WOODENHELMET);
            WoodenHelmetWear = Load(Main.TextureLibrary.ITEMS_ARMOURS_HEADS_WOODENHELMETWEAR);
            WoodenChestplate = Load(Main.TextureLibrary.ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATE);
            WoodenChestplateWear = Load(Main.TextureLibrary.ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATEWEAR);
            WoodenLeggings = Load(Main.TextureLibrary.ITEMS_ARMOURS_LEGS_WOODENLEGGINGS);
            WoodenLeggingsWear = Load(Main.TextureLibrary.ITEMS_ARMOURS_LEGS_WOODENLEGGINGSWEAR);
            WoodenBoots = Load(Main.TextureLibrary.ITEMS_ARMOURS_FEET_WOODENBOOTS);
            WoodenBootsWear = Load(Main.TextureLibrary.ITEMS_ARMOURS_FEET_WOODENBOOTSWEAR);
        }
    }
}