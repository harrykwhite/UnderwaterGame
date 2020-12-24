namespace UnderwaterGame.Sprites
{
    using System.Collections.Generic;

    public partial class Sprite
    {
        public static List<Sprite> sprites = new List<Sprite>();

        public static Sprite seaweed;

        public static Sprite spawnStatue;

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

        public static Sprite woodenTrident;

        public static Sprite woodenSword;

        public static Sprite woodenBow;

        public static Sprite crabGrenade;

        public static Sprite crabShuriken;

        public static Sprite healthKelp;

        public static Sprite pinkJelly;

        public static Sprite purpleJelly;

        public static Sprite stoneHelmet;

        public static Sprite stoneHelmetWear;

        public static Sprite stoneChestplate;

        public static Sprite stoneChestplateWear;

        public static Sprite stoneLeggings;

        public static Sprite stoneLeggingsWear;

        public static Sprite stoneBoots;

        public static Sprite stoneBootsWear;

        public static void LoadAll()
        {
            seaweed = Load(Main.textureLibrary.ENVIRONMENTALS_SEAWEED_SEAWEED);
            spawnStatue = Load(Main.textureLibrary.ENVIRONMENTALS_STATUES_SPAWNSTATUE);
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
            woodenTrident = Load(Main.textureLibrary.ITEMS_WEAPONS_MELEE_TRIDENTS_WOODENTRIDENT);
            woodenSword = Load(Main.textureLibrary.ITEMS_WEAPONS_MELEE_SWORDS_WOODENSWORD);
            woodenBow = Load(Main.textureLibrary.ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW);
            crabGrenade = Load(Main.textureLibrary.ITEMS_WEAPONS_RANGED_THROWABLES_CRABGRENADE);
            crabShuriken = Load(Main.textureLibrary.ITEMS_WEAPONS_RANGED_THROWABLES_CRABSHURIKEN);
            healthKelp = Load(Main.textureLibrary.ITEMS_EDIBLES_HEALTHKELP);
            pinkJelly = Load(Main.textureLibrary.ITEMS_EDIBLES_PINKJELLY);
            purpleJelly = Load(Main.textureLibrary.ITEMS_EDIBLES_PURPLEJELLY);
            stoneHelmet = Load(Main.textureLibrary.ITEMS_ARMOURS_HEADS_STONEHELMET);
            stoneHelmetWear = Load(Main.textureLibrary.ITEMS_ARMOURS_HEADS_STONEHELMETWEAR);
            stoneChestplate = Load(Main.textureLibrary.ITEMS_ARMOURS_CHESTS_STONECHESTPLATE);
            stoneChestplateWear = Load(Main.textureLibrary.ITEMS_ARMOURS_CHESTS_STONECHESTPLATEWEAR);
            stoneLeggings = Load(Main.textureLibrary.ITEMS_ARMOURS_LEGS_STONELEGGINGS);
            stoneLeggingsWear = Load(Main.textureLibrary.ITEMS_ARMOURS_LEGS_STONELEGGINGSWEAR);
            stoneBoots = Load(Main.textureLibrary.ITEMS_ARMOURS_FEET_STONEBOOTS);
            stoneBootsWear = Load(Main.textureLibrary.ITEMS_ARMOURS_FEET_STONEBOOTSWEAR);
        }
    }
}