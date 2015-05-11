namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;
    using NegroniGame.SystemFunctions;
    using NegroniGame.Toolbar;
    using System;

    public sealed class InfoBoxes
    {
        // Singleton !
        private static InfoBoxes instance;

        private InfoBoxes()
        {
            this.Font = GameScreen.Instance.FontInfoBox;

            this.TextPosition = new Vector2(0, 0);

            this.InfoBoxTexture = GameScreen.Instance.InfoBoxTexture;

            this.InventoryPopUpInfoBoxText = "";
        }

        public static InfoBoxes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InfoBoxes();
                }
                return instance;
            }
        }
        
        # region Properties declaration

        public Rectangle InventoryPopUpInfoBox { get; private set; }
        public string InventoryPopUpInfoBoxText { get; private set; }
        public Texture2D InfoBoxTexture { get; private set; }
        public Vector2 TextPosition { get; private set; }
        public SpriteFont Font { get; private set; }
        private int StringLength { get; set; }

        public Rectangle PlayerNameRectangle { get; private set; }
        public Rectangle MarketInfoRectangle { get; private set; }
        public Rectangle WellInfoRectangle { get; private set; }
        public Rectangle PlayerInfoRectangle { get; private set; }
        public Rectangle HpInfoRectangle { get; private set; }
        public Rectangle NpcSorcererInfoRectangle { get; private set; }
        public Rectangle MobInfoRectangle { get; private set; }

        public Point MousePosition { get; private set; }
        public string BoxNameText { get; private set; }
        public string BoxMarketText { get; private set; }
        public string BoxWellText { get; private set; }
        public string PlayerInfoText { get; private set; }
        public string BoxAtkDefText { get; private set; }
        public string HpInfoText { get; private set; }
        public string NpcSorcererInfoText { get; private set; }
        public string MobInfoText { get; private set; }

		# endregion

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            MousePosition = new Point(mouseState.X, mouseState.Y);

            if (Handlers.SceneryHandler.Instance.PlayerPicRectangle.Contains(MousePosition))
            {
                this.PlayerNameRectangle = new Rectangle(MousePosition.X + 25, MousePosition.Y - 10, 100, 50);
                this.BoxNameText = Player.Instance.Name;
                this.BoxAtkDefText = String.Format("Atk. {0}\nDef. {1}", Player.Instance.WeaponDmg, Player.Instance.ArmorDef);
            }
            else
            {
                this.PlayerNameRectangle = new Rectangle(0, 0, 0, 0);
                this.BoxNameText = "";
                this.BoxAtkDefText = "";
            }

            if (Handlers.SceneryHandler.Instance.MarketPosition.Contains(MousePosition))
            {
                this.MarketInfoRectangle = new Rectangle(MousePosition.X - 40, MousePosition.Y + 20, 80, 30);
                this.BoxMarketText = "Market\nBuy Items";
            }
            else
            {
                this.MarketInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.BoxMarketText = "";
            }

            if (Well.Instance.WellPosition.Contains(MousePosition))
            {
                this.WellInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y + 10, 80, 30);
                this.BoxWellText = "Well\nRestores HP";
            }
            else
            {
                this.WellInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.BoxWellText = "";
            }

            if (Player.Instance.DestinationPosition.Contains(MousePosition))
            {
                this.PlayerInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y + 10, 80, 30);
                this.PlayerInfoText = "It's\nYou!";
            }
            else
            {
                this.PlayerInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.PlayerInfoText = "";
            }

            if (NpcSorcerer.Instance.DrawRect.Contains(MousePosition))
            {
                this.NpcSorcererInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y + 10, 80, 30);
                this.NpcSorcererInfoText = "NPC Sorcerer\nRent Helper";
            }
            else
            {
                this.NpcSorcererInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.NpcSorcererInfoText = "";
            }

            // hp
            if (Toolbar.HP.Instance.HpPosition.Contains(MousePosition))
            {
                this.HpInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y, 80, 30);
                this.HpInfoText = String.Format("HP\n{0} / {1}", Player.Instance.HpPointsCurrent, GameSettings.HP_POINTS_INITIAL);
            }
            else
            {
                this.HpInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.HpInfoText = "";
            }

            // Mobs
            if (Handlers.MonstersHandler.Instance.SpawnedMobs == null || Handlers.MonstersHandler.Instance.SpawnedMobs.Count == 0)
            {
                this.MobInfoRectangle = new Rectangle(0, 0, 0, 0);
                this.MobInfoText = "";
            }
            else
            {
                foreach (var monster in Handlers.MonstersHandler.Instance.SpawnedMobs)
                {
                    if (monster.DestinationPosition.Contains(MousePosition))
                    {
                        this.MobInfoRectangle = new Rectangle(MousePosition.X + 20, MousePosition.Y, 80, 30);
                        this.MobInfoText = String.Format("{0}\nAtk.{1}", monster.Name, monster.Attack);
                        break;
                    }
                    else
                    {
                        this.MobInfoRectangle = new Rectangle(0, 0, 0, 0);
                        this.MobInfoText = "";
                    }
                }
            }

            // Show small pop-up descriptive text box when mouse is over an item in the inventory
            if (InventorySlots.Instance.Slot1CoinsArea.Contains(MousePosition))
            {
                this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y + 20, 100, 40);

                this.InventoryPopUpInfoBoxText = String.Format("Coins" + "\n" + "{0}", Player.Instance.Coins.Amount); ////////////////

                this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                this.TextPosition = new Vector2(MousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
            }

            else if (InventorySlots.Instance.Slot2ElixirsArea.Contains(MousePosition) && Player.Instance.Elixirs.Count > 0)
            {
                if (mouseState.LeftButton != ButtonState.Pressed)
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y + 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0} HP Elixir(s)" + "\n" + "Restores {1} HP", Player.Instance.Elixirs.Count, GameSettings.RECOVERY_AMOUNT);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X + 25 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
                }
            }

            else if (InventorySlots.Instance.Slot3WeaponArea.Contains(MousePosition) && Player.Instance.Weapon != null)
            {
                if (mouseState.RightButton != ButtonState.Pressed)
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y + 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Atk. {1}", Player.Instance.Weapon.Name, Player.Instance.Weapon.Attack);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
                }
            }

            else if (InventorySlots.Instance.Slot4ShieldArea.Contains(MousePosition) && Player.Instance.Shield != null)
            {
                if (mouseState.RightButton != ButtonState.Pressed)
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y + 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Shield.Name, Player.Instance.Shield.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y + 25);
                }
            }

            else if (InventorySlots.Instance.Slot5HelmetArea.Contains(MousePosition) && Player.Instance.Helmet != null)
            {
                if (mouseState.RightButton != ButtonState.Pressed)
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Helmet.Name, Player.Instance.Helmet.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else if (InventorySlots.Instance.Slot6RobeArea.Contains(MousePosition) && Player.Instance.Robe != null)
            {
                if (mouseState.RightButton != ButtonState.Pressed)
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X + 20, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Robe.Name, Player.Instance.Robe.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X + 20 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else if (InventorySlots.Instance.Slot7GlovesArea.Contains(MousePosition) && Player.Instance.Gloves != null)
            {
                if (mouseState.RightButton != ButtonState.Pressed)
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Gloves.Name, Player.Instance.Gloves.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else if (InventorySlots.Instance.Slot8BootsArea.Contains(MousePosition) && Player.Instance.Boots != null)
            {
                if (mouseState.RightButton != ButtonState.Pressed)
                {
                    this.InventoryPopUpInfoBox = new Rectangle(MousePosition.X - 80, MousePosition.Y - 20, 100, 40);

                    this.InventoryPopUpInfoBoxText = String.Format("{0}" + "\n" + "Def. {1}", Player.Instance.Boots.Name, Player.Instance.Boots.Defence);
                    this.StringLength = (int)this.Font.MeasureString(this.InventoryPopUpInfoBoxText).X;

                    this.TextPosition = new Vector2(MousePosition.X - 80 + ((InventoryPopUpInfoBox.Width - StringLength) / 2), MousePosition.Y - 15);
                }
            }

            else
            {
                this.InventoryPopUpInfoBox = new Rectangle(0, 0, 0, 0);
                this.TextPosition = new Vector2(0, 0);
                this.InventoryPopUpInfoBoxText = "";
            }
        }


        public void Draw()
        {
            new Sprite(GameScreen.Instance.InfoBox1Texture, PlayerNameRectangle).DrawBox();
            new Sprite(GameScreen.Instance.FontMessages, this.BoxNameText, new Vector2(PlayerNameRectangle.X + 20, PlayerNameRectangle.Y + 6)).DrawText();
            new Sprite(GameScreen.Instance.FontInfoBox, this.BoxAtkDefText, new Vector2(PlayerNameRectangle.X + 20, PlayerNameRectangle.Y + 21)).DrawText();

            new Sprite(GameScreen.Instance.InfoBox1Texture, MarketInfoRectangle).DrawBox();
            new Sprite(GameScreen.Instance.FontInfoBox, this.BoxMarketText, new Vector2(MarketInfoRectangle.X + 20, MarketInfoRectangle.Y + 2)).DrawText();

            new Sprite(GameScreen.Instance.InfoBox1Texture, WellInfoRectangle).DrawBox();
            new Sprite(GameScreen.Instance.FontInfoBox, this.BoxWellText, new Vector2(WellInfoRectangle.X + 15, WellInfoRectangle.Y + 2)).DrawText();

            new Sprite(GameScreen.Instance.InfoBox1Texture, PlayerInfoRectangle).DrawBox();
            new Sprite(GameScreen.Instance.FontMessages, this.PlayerInfoText, new Vector2(PlayerInfoRectangle.X + 25, PlayerInfoRectangle.Y + 1)).DrawText();

            new Sprite(GameScreen.Instance.InfoBox1Texture, HpInfoRectangle).DrawBox();
            new Sprite(GameScreen.Instance.FontMessages, this.HpInfoText, new Vector2(HpInfoRectangle.X + 10, HpInfoRectangle.Y)).DrawText();

            new Sprite(GameScreen.Instance.InfoBox1Texture, NpcSorcererInfoRectangle).DrawBox();
            new Sprite(GameScreen.Instance.FontInfoBox, this.NpcSorcererInfoText, new Vector2(NpcSorcererInfoRectangle.X + 15, NpcSorcererInfoRectangle.Y + 2)).DrawText();

            new Sprite(GameScreen.Instance.InfoBox1Texture, MobInfoRectangle).DrawBox();
            new Sprite(GameScreen.Instance.FontInfoBox, this.MobInfoText, new Vector2(MobInfoRectangle.X + 15, MobInfoRectangle.Y + 2)).DrawText();

            new Sprite(this.InfoBoxTexture, this.InventoryPopUpInfoBox).DrawBox();
            new Sprite(this.Font, this.InventoryPopUpInfoBoxText, this.TextPosition).DrawText(); 
        }
    }
}
