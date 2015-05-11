namespace NegroniGame.Toolbar
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class InventorySlots
    {
        // Singleton !
        private static InventorySlots instance;

        public static InventorySlots Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InventorySlots();
                }
                return instance;
            }
        }

        private InventorySlots()
        {
            this.DefaultSlotTextures = GameScreen.Instance.SlotsTextures;

            InventoryAreaTopPoint = GameScreen.ScreenHeight - 105;
            InventoryAreaLeftPoint = 485;
            InventorySlotWidth = InventorySlotHeight = 50;

            this.InventoryArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint, 206, 100); // the whole inventory

            // 1 is added for borders
            // first row of slots
            this.Slot1CoinsArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot2ElixirsArea = new Rectangle(InventoryAreaLeftPoint + InventorySlotWidth + 1, InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot3WeaponArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 2), InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot4ShieldArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 3), InventoryAreaTopPoint,
                                                    InventorySlotWidth, InventorySlotHeight);
            // second row of slots
            this.Slot5HelmetArea = new Rectangle(InventoryAreaLeftPoint, InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot6RobeArea = new Rectangle(InventoryAreaLeftPoint + InventorySlotWidth + 1, InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot7GlovesArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 2), InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);
            this.Slot8BootsArea = new Rectangle(InventoryAreaLeftPoint + ((InventorySlotWidth + 1) * 3), InventoryAreaTopPoint + InventorySlotHeight + 1,
                                                    InventorySlotWidth, InventorySlotHeight);

            //default textures for inventory slots
            Slot1Image = DefaultSlotTextures[0];
            Slot2Image = DefaultSlotTextures[1];
            Slot3Image = DefaultSlotTextures[2];
            Slot4Image = DefaultSlotTextures[3];
            Slot5Image = DefaultSlotTextures[4];
            Slot6Image = DefaultSlotTextures[5];
            Slot7Image = DefaultSlotTextures[6];
            Slot8Image = DefaultSlotTextures[7];
        }

        private int InventorySlotWidth { get; set; }
        private int InventorySlotHeight { get; set; }
        private int InventoryAreaTopPoint { get; set; }
        private int InventoryAreaLeftPoint { get; set; }
        public Rectangle InventoryArea { get; private set; }
        public Rectangle Slot1CoinsArea { get; private set; }
        public Rectangle Slot2ElixirsArea { get; private set; }
        public Rectangle Slot3WeaponArea { get; private set; }
        public Rectangle Slot4ShieldArea { get; private set; }
        public Rectangle Slot5HelmetArea { get; private set; }
        public Rectangle Slot6RobeArea { get; private set; }
        public Rectangle Slot7GlovesArea { get; private set; }
        public Rectangle Slot8BootsArea { get; private set; }
        public List<Texture2D> DefaultSlotTextures { get; private set; }
        public Texture2D Slot1Image { get; private set; }
        public Texture2D Slot2Image { get; private set; }
        public Texture2D Slot3Image { get; private set; }
        public Texture2D Slot4Image { get; private set; }
        public Texture2D Slot5Image { get; private set; }
        public Texture2D Slot6Image { get; private set; }
        public Texture2D Slot7Image { get; private set; }
        public Texture2D Slot8Image { get; private set; }
        public Point MousePosition { get; private set; }
        public bool IsAnyWeapon { get; private set; }
        public bool IsAnyShield { get; private set; }
        public bool IsAnyHelmet { get; private set; }
        public bool IsAnyRobe { get; private set; }
        public bool IsAnyGloves { get; private set; }
        public bool IsAnyBoots { get; private set; }


        public void Update(GameTime gameTime, MouseState mouseState)
        {
            MousePosition = new Point(mouseState.X, mouseState.Y);

            CheckItems();

            if (this.Slot2ElixirsArea.Contains(MousePosition) && Player.Instance.Elixirs.Count > 0)
            {
                // Checks if left mouse button is clicked and drinks the elixir
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before drinking the elixir

                    if (Handlers.ElixirsHandler.Instance.UseElixir())
                    {
                        Player.Instance.DrinkElixir();
                    }
                }
            }

            else if (this.Slot3WeaponArea.Contains(MousePosition) && IsAnyWeapon)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.DestroyItem("weapon");
                }
            }

            else if (this.Slot4ShieldArea.Contains(MousePosition) && IsAnyShield)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.DestroyItem("shield");
                }
            }

            else if (this.Slot5HelmetArea.Contains(MousePosition) && IsAnyHelmet)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.DestroyItem("helmet");
                }
            }

            else if (this.Slot6RobeArea.Contains(MousePosition) && IsAnyRobe)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.DestroyItem("robe");
                }
            }

            else if (this.Slot7GlovesArea.Contains(MousePosition) && IsAnyGloves)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.DestroyItem("gloves");
                }
            }

            else if (this.Slot8BootsArea.Contains(MousePosition) && IsAnyBoots)
            {
                // Checks if right mouse button is clicked and destroys the item
                if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // TO DO: pop up box ask if sure before destroys the item
                    Player.Instance.DestroyItem("boots");
                }
            }

            CheckItems();

            // Show different picture on the slot depending on the object there
            if (Player.Instance.Coins.Amount > 0) { Slot1Image = Player.Instance.Coins.Texture; }
            else { Slot1Image = DefaultSlotTextures[0]; }

            if (Player.Instance.Elixirs.Count > 0) { Slot2Image = Player.Instance.Elixirs.Texture; }
            else { Slot2Image = DefaultSlotTextures[1]; }

            if (IsAnyWeapon) { Slot3Image = Player.Instance.Weapon.Texture; }
            else { Slot3Image = DefaultSlotTextures[2]; }

            if (IsAnyShield) { Slot4Image = Player.Instance.Shield.Texture; }
            else { Slot4Image = DefaultSlotTextures[3]; }

            if (IsAnyHelmet) { Slot5Image = Player.Instance.Helmet.Texture; }
            else { Slot5Image = DefaultSlotTextures[4]; }

            if (IsAnyRobe) { Slot6Image = Player.Instance.Robe.Texture; }
            else { Slot6Image = DefaultSlotTextures[5]; }

            if (IsAnyGloves) { Slot7Image = Player.Instance.Gloves.Texture; }
            else { Slot7Image = DefaultSlotTextures[6]; }

            if (IsAnyBoots) { Slot8Image = Player.Instance.Boots.Texture; }
            else { Slot8Image = DefaultSlotTextures[7]; }
        }

        public void Draw()
        {
            new SystemFunctions.Sprite(this.Slot1Image, this.Slot1CoinsArea).DrawBox();
            new SystemFunctions.Sprite(this.Slot2Image, this.Slot2ElixirsArea).DrawBox();
            new SystemFunctions.Sprite(this.Slot3Image, this.Slot3WeaponArea).DrawBox();
            new SystemFunctions.Sprite(this.Slot4Image, this.Slot4ShieldArea).DrawBox();
            new SystemFunctions.Sprite(this.Slot5Image, this.Slot5HelmetArea).DrawBox();
            new SystemFunctions.Sprite(this.Slot6Image, this.Slot6RobeArea).DrawBox();
            new SystemFunctions.Sprite(this.Slot7Image, this.Slot7GlovesArea).DrawBox();
            new SystemFunctions.Sprite(this.Slot8Image, this.Slot8BootsArea).DrawBox();
        }

        private void CheckItems()
        {
            IsAnyWeapon = (Player.Instance.Weapon != null) ? true : false;
            IsAnyShield = (Player.Instance.Shield != null) ? true : false;
            IsAnyHelmet = (Player.Instance.Helmet != null) ? true : false;
            IsAnyRobe = (Player.Instance.Robe != null) ? true : false;
            IsAnyGloves = (Player.Instance.Gloves != null) ? true : false;
            IsAnyBoots = (Player.Instance.Boots != null) ? true : false;
        }
       
    }
}
