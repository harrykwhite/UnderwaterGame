using Microsoft.Xna.Framework.Graphics;

namespace UnderwaterGame.Assets
{
    public class TextureLibrary : Library<Texture2D>
    {
        public readonly LibraryAsset TILES_SOLIDS_STONE = new LibraryAsset("Textures/Tiles/Solids/Stone");
        public readonly LibraryAsset TILES_SOLIDS_SAND = new LibraryAsset("Textures/Tiles/Solids/Sand");
        public readonly LibraryAsset TILES_SOLIDS_BRICK = new LibraryAsset("Textures/Tiles/Solids/Brick");
        public readonly LibraryAsset TILES_LIQUIDS_WATER = new LibraryAsset("Textures/Tiles/Liquids/Water");

        public readonly LibraryAsset ENVIRONMENTALS_BIGSEAWEED = new LibraryAsset("Textures/Environmentals/BigSeaweed");
        public readonly LibraryAsset ENVIRONMENTALS_SMALLSEAWEED = new LibraryAsset("Textures/Environmentals/SmallSeaweed");

        public readonly LibraryAsset CHARACTERS_ENEMIES_JELLYFISH_JELLYFISH = new LibraryAsset("Textures/Characters/Enemies/Jellyfish/Jellyfish");
        public readonly LibraryAsset CHARACTERS_ENEMIES_JELLYFISH_TALLJELLYFISH = new LibraryAsset("Textures/Characters/Enemies/Jellyfish/TallJellyfish");
        public readonly LibraryAsset CHARACTERS_PLAYER_PLAYERIDLE = new LibraryAsset("Textures/Characters/Player/PlayerIdle");
        public readonly LibraryAsset CHARACTERS_PLAYER_PLAYERSWIM = new LibraryAsset("Textures/Characters/Player/PlayerSwim");

        public readonly LibraryAsset EFFECTS_LONGSWING = new LibraryAsset("Textures/Effects/LongSwing");
        public readonly LibraryAsset EFFECTS_WIDESWING = new LibraryAsset("Textures/Effects/WideSwing");

        public readonly LibraryAsset PARTICLES_BLOOD = new LibraryAsset("Textures/Particles/Blood");
        public readonly LibraryAsset PARTICLES_BUBBLE0 = new LibraryAsset("Textures/Particles/Bubble0");
        public readonly LibraryAsset PARTICLES_BUBBLE1 = new LibraryAsset("Textures/Particles/Bubble1");
        public readonly LibraryAsset PARTICLES_BUBBLE2 = new LibraryAsset("Textures/Particles/Bubble2");
        public readonly LibraryAsset PARTICLES_SMOKE = new LibraryAsset("Textures/Particles/Smoke");
        public readonly LibraryAsset PARTICLES_WOOD = new LibraryAsset("Textures/Particles/Wood");
        public readonly LibraryAsset PARTICLES_CRABSHELL = new LibraryAsset("Textures/Particles/CrabShell");
        public readonly LibraryAsset PARTICLES_LIQUID = new LibraryAsset("Textures/Particles/Liquid");
        public readonly LibraryAsset PARTICLES_FIRE = new LibraryAsset("Textures/Particles/Fire");

        public readonly LibraryAsset PROJECTILES_ARROWS_WOODENARROW = new LibraryAsset("Textures/Projectiles/Arrows/WoodenArrow");
        public readonly LibraryAsset PROJECTILES_MAGIC_FLAREMAGIC = new LibraryAsset("Textures/Projectiles/Magic/FlareMagic");

        public readonly LibraryAsset ITEMS_WEAPONS_MELEE_TRIDENTS_WOODENTRIDENT = new LibraryAsset("Textures/Items/Weapons/Melee/Tridents/WoodenTrident");
        public readonly LibraryAsset ITEMS_WEAPONS_MELEE_SWORDS_WOODENSWORD = new LibraryAsset("Textures/Items/Weapons/Melee/Swords/WoodenSword");

        public readonly LibraryAsset ITEMS_WEAPONS_RANGED_BOWS_WOODENBOW = new LibraryAsset("Textures/Items/Weapons/Ranged/Bows/WoodenBow");
        public readonly LibraryAsset ITEMS_WEAPONS_RANGED_THROWABLES_CRABGRENADE = new LibraryAsset("Textures/Items/Weapons/Ranged/Throwables/CrabGrenade");
        public readonly LibraryAsset ITEMS_WEAPONS_RANGED_THROWABLES_CRABSHURIKEN = new LibraryAsset("Textures/Items/Weapons/Ranged/Throwables/CrabShuriken");

        public readonly LibraryAsset ITEMS_WEAPONS_MAGIC_WANDS_WOODENWAND = new LibraryAsset("Textures/Items/Weapons/Magic/Wands/WoodenWand");

        public readonly LibraryAsset ITEMS_CONSUMABLES_HEALINGKELP = new LibraryAsset("Textures/Items/Consumables/HealingKelp");

        public readonly LibraryAsset ITEMS_ARMOURS_HEADS_WOODENHELMET = new LibraryAsset("Textures/Items/Armours/Heads/WoodenHelmet");
        public readonly LibraryAsset ITEMS_ARMOURS_HEADS_WOODENHELMETWEAR = new LibraryAsset("Textures/Items/Armours/Heads/WoodenHelmetWear");
        public readonly LibraryAsset ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATE = new LibraryAsset("Textures/Items/Armours/Chests/WoodenChestplate");
        public readonly LibraryAsset ITEMS_ARMOURS_CHESTS_WOODENCHESTPLATEWEAR = new LibraryAsset("Textures/Items/Armours/Chests/WoodenChestplateWear");
        public readonly LibraryAsset ITEMS_ARMOURS_LEGS_WOODENLEGGINGS = new LibraryAsset("Textures/Items/Armours/Legs/WoodenLeggings");
        public readonly LibraryAsset ITEMS_ARMOURS_LEGS_WOODENLEGGINGSWEAR = new LibraryAsset("Textures/Items/Armours/Legs/WoodenLeggingsWear");
        public readonly LibraryAsset ITEMS_ARMOURS_FEET_WOODENBOOTS = new LibraryAsset("Textures/Items/Armours/Feet/WoodenBoots");
        public readonly LibraryAsset ITEMS_ARMOURS_FEET_WOODENBOOTSWEAR = new LibraryAsset("Textures/Items/Armours/Feet/WoodenBootsWear");

        public readonly LibraryAsset UI_BUTTONS_OTHER_BUTTON = new LibraryAsset("Textures/UI/Buttons/Other/Button");
        public readonly LibraryAsset UI_BUTTONS_ICONS_INVENTORY_WIELDICON = new LibraryAsset("Textures/UI/Buttons/Icons/Inventory/WieldIcon");
        public readonly LibraryAsset UI_BUTTONS_ICONS_INVENTORY_HOTBARICON = new LibraryAsset("Textures/UI/Buttons/Icons/Inventory/HotbarIcon");
        public readonly LibraryAsset UI_BUTTONS_ICONS_INVENTORY_ARMOURICON = new LibraryAsset("Textures/UI/Buttons/Icons/Inventory/ArmourIcon");
        public readonly LibraryAsset UI_BUTTONS_ICONS_INVENTORY_CRAFTINGICON = new LibraryAsset("Textures/UI/Buttons/Icons/Inventory/CraftingIcon");
        public readonly LibraryAsset UI_BUTTONS_ICONS_OTHER_BACKICON = new LibraryAsset("Textures/UI/Buttons/Icons/Other/BackIcon");
        public readonly LibraryAsset UI_BUTTONS_ICONS_OTHER_OPTIONSICON = new LibraryAsset("Textures/UI/Buttons/Icons/Other/OptionsIcon");
        public readonly LibraryAsset UI_BUTTONS_ICONS_OTHER_CONTINUEICON = new LibraryAsset("Textures/UI/Buttons/Icons/Other/ContinueIcon");
        public readonly LibraryAsset UI_BUTTONS_ICONS_OTHER_PRODUCEICON = new LibraryAsset("Textures/UI/Buttons/Icons/Other/ProduceIcon");
        public readonly LibraryAsset UI_SLIDER_BAR = new LibraryAsset("Textures/UI/Slider/Bar");
        public readonly LibraryAsset UI_SLIDER_BALL = new LibraryAsset("Textures/UI/Slider/Ball");
        public readonly LibraryAsset UI_OTHER_CURSOR = new LibraryAsset("Textures/UI/Other/Cursor");

        public readonly LibraryAsset OTHER_PIXEL = new LibraryAsset("Textures/Other/Pixel");
    }
}