namespace UnderwaterGame.Sprites
{
    using System.Collections.Generic;

    public partial class Sprite
    {
        public static List<Sprite> sprites = new List<Sprite>();

        public static Sprite bigSeaweed;

        public static Sprite smallSeaweed;

        public static Sprite jellyfish;

        public static Sprite tallJellyfish;

        public static Sprite playerIdle;

        public static Sprite playerSwim;

        public static Sprite longSwing;

        public static Sprite wideSwing;

        public static Sprite blood;

        public static Sprite bubble;

        public static Sprite smoke;

        public static Sprite wood;

        public static Sprite crabShell;

        public static Sprite liquid;

        public static Sprite fire;

        public static Sprite woodenArrow;

        public static Sprite flareMagic;

        public static Sprite woodenTrident;

        public static Sprite woodenSword;

        public static Sprite woodenBow;

        public static Sprite crabGrenade;

        public static Sprite crabShuriken;

        public static Sprite woodenWand;

        public static Sprite healthKelp;

        public static Sprite magicKelp;

        public static Sprite woodenHelmet;

        public static Sprite woodenHelmetWear;

        public static Sprite woodenChestplate;

        public static Sprite woodenChestplateWear;

        public static Sprite woodenLeggings;

        public static Sprite woodenLeggingsWear;

        public static Sprite woodenBoots;

        public static Sprite woodenBootsWear;
        
        public static Sprite cloud;

        public static void LoadAll()
        {
            bigSeaweed = Load(Main.textureLibrary.ENVIRONMENTALS_BIGSEAWEED);
            smallSeaweed = Load(Main.textureLibrary.ENVIRONMENTALS_SMALLSEAWEED);
            jellyfish = Load(Main.textureLibrary.CHARACTERS_ENEMIES_JELLYFISH_JELLYFISH);
            tallJellyfish = Load(Main.textureLibrary.CHARACTERS_ENEMIES_JELLYFISH_TALLJELLYFISH);
            playerIdle = Load(Main.textureLibrary.CHARACTERS_PLAYER_PLAYERIDLE);
            playerSwim = Load(Main.textureLibrary.CHARACTERS_PLAYER_PLAYERSWIM);
            longSwing = Load(Main.textureLibrary.EFFECTS_LONGSWING);
            wideSwing = Load(Main.textureLibrary.EFFECTS_WIDESWING);
            blood = Load(Main.textureLibrary.PARTICLES_BLOOD);
            bubble = Load(Main.textureLibrary.PARTICLES_BUBBLE);
            smoke = Load(Main.textureLibrary.PARTICLES_SMOKE);
            wood = Load(Main.textureLibrary.PARTICLES_WOOD);
            crabShell = Load(Main.textureLibrary.PARTICLES_CRABSHELL);
            liquid = Load(Main.textureLibrary.PARTICLES_LIQUID);
            fire = Load(Main.textureLibrary.PARTICLES_FIRE);
            woodenArrow = Load(Main.textureLibrary.PROJECTILES_ARROWS_WOODENARROW);
            flareMagic = Load(Main.textureLibrary.PROJECTILES_MAGIC_FLAREMAGIC);
            woodenTrident = Load(Main.textureLibrary.ITEMS_WEAPONS_MELEE_TRIDENTS_WOODENTRIDENT);
            woodenSword = Load(Main.textureLibrary.ITEMS_WEAPONS_MELEE_SWORDS_WOODENSWORD);
            woodenBow = Load(Main.textureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW);
            crabGrenade = Load(Main.textureLibrary.ITEMS_WEAPONS_RANGED_THROWABLES_CRABGRENADE);
            crabShuriken = Load(Main.textureLibrary.ITEMS_WEAPONS_RANGED_THROWABLES_CRABSHURIKEN);
            woodenWand = Load(Main.textureLibrary.ITEMS_WEAPONS_MAGIC_WANDS_WOODENWAND);
            healthKelp = Load(Main.textureLibrary.ITEMS_CONSUMABLES_HEALTHKELP);
            magicKelp = Load(Main.textureLibrary.ITEMS_CONSUMABLES_MAGICKELP);
            woodenHelmet = Load(Main.textureLibrary.ITEMS_ARMOURS_HEADS_WOODENHELMET);
            woodenHelmetWear = Load(Main.textureLibrary.ITEMS_ARMOURS_HEADS_WOODENHELMETWEAR);
            woodenChestplate = Load(Main.textureLibrary.ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATE);
            woodenChestplateWear = Load(Main.textureLibrary.ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATEWEAR);
            woodenLeggings = Load(Main.textureLibrary.ITEMS_ARMOURS_LEGS_WOODENLEGGINGS);
            woodenLeggingsWear = Load(Main.textureLibrary.ITEMS_ARMOURS_LEGS_WOODENLEGGINGSWEAR);
            woodenBoots = Load(Main.textureLibrary.ITEMS_ARMOURS_FEET_WOODENBOOTS);
            woodenBootsWear = Load(Main.textureLibrary.ITEMS_ARMOURS_FEET_WOODENBOOTSWEAR);
            cloud = Load(Main.textureLibrary.OTHER_CLOUD);
        }
    }
}