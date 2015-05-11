namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;

    public sealed class MarketDialog
    {
        // Singleton !
        private static MarketDialog instance;

        private static readonly int buyButtonSize = 50;

        private static readonly int dialogWindowWidth = 250;
        private static readonly int dialogWindowHeight = 370;

        private static readonly Texture2D dialogWindowTexture = GameScreen.Instance.MarketDialog;

        private static readonly Rectangle dialogWindowPosition = new Rectangle((GameScreen.ScreenWidth - dialogWindowWidth) / 2, 10, dialogWindowWidth, dialogWindowHeight);
        private static readonly Rectangle item1PicPosition = new Rectangle(dialogWindowPosition.Left + 10, dialogWindowPosition.Top + 60, 50, 50);
        private static readonly Rectangle item2PicPosition = new Rectangle(dialogWindowPosition.Left + 10, dialogWindowPosition.Top + 100, 50, 50);
        private static readonly Rectangle item3PicPosition = new Rectangle(dialogWindowPosition.Left + 10, dialogWindowPosition.Top + 140, 50, 50);
        private static readonly Rectangle item4PicPosition = new Rectangle(dialogWindowPosition.Left + 10, dialogWindowPosition.Top + 180, 50, 50);
        private static readonly Rectangle item5PicPosition = new Rectangle(dialogWindowPosition.Left + 10, dialogWindowPosition.Top + 220, 50, 50);
        private static readonly Rectangle item6PicPosition = new Rectangle(dialogWindowPosition.Left + 10, dialogWindowPosition.Top + 260, 50, 50);
        private static readonly Rectangle item7PicPosition = new Rectangle(dialogWindowPosition.Left + 10, dialogWindowPosition.Top + 300, 50, 50);

        private static readonly Texture2D buyButtontexture = GameScreen.Instance.BuyButton;
        private readonly Rectangle buyButton1Position = new Rectangle(dialogWindowPosition.Right - buyButtonSize - 10, item1PicPosition.Top, buyButtonSize, buyButtonSize);
        private readonly Rectangle buyButton2Position = new Rectangle(dialogWindowPosition.Right - buyButtonSize - 10, item2PicPosition.Top, buyButtonSize, buyButtonSize);
        private readonly Rectangle buyButton3Position = new Rectangle(dialogWindowPosition.Right - buyButtonSize - 10, item3PicPosition.Top, buyButtonSize, buyButtonSize);
        private readonly Rectangle buyButton4Position = new Rectangle(dialogWindowPosition.Right - buyButtonSize - 10, item4PicPosition.Top, buyButtonSize, buyButtonSize);
        private readonly Rectangle buyButton5Position = new Rectangle(dialogWindowPosition.Right - buyButtonSize - 10, item5PicPosition.Top, buyButtonSize, buyButtonSize);
        private readonly Rectangle buyButton6Position = new Rectangle(dialogWindowPosition.Right - buyButtonSize - 10, item6PicPosition.Top, buyButtonSize, buyButtonSize);
        private readonly Rectangle buyButton7Position = new Rectangle(dialogWindowPosition.Right - buyButtonSize - 10, item7PicPosition.Top, buyButtonSize, buyButtonSize);

        private readonly Items.ElixirsHP item1 = new Items.ElixirsHP(1);
        private readonly Items.Armor.MajesticBoots item2 = new Items.Armor.MajesticBoots();
        private readonly Items.Armor.MajesticGloves item3 = new Items.Armor.MajesticGloves();
        private readonly Items.Armor.MajesticHelmet item4 = new Items.Armor.MajesticHelmet();
        private readonly Items.Armor.MajesticRobe item5 = new Items.Armor.MajesticRobe();
        private readonly Items.Armor.MajesticShield item6 = new Items.Armor.MajesticShield();
        private readonly Items.Weapon.MysticStaff item7 = new Items.Weapon.MysticStaff();

        private readonly string elixirText, bootsText, glovesText, helmetText, robeText, shieldText, staffText;
        private static readonly SpriteFont font = GameScreen.Instance.FontMessages;

        private MarketDialog()
        {
            this.elixirText = String.Format("{0}" + "\n" + "HP {1} / {2} Coins", this.item1.Name, GameSettings.RECOVERY_AMOUNT, this.item1.BuyingPrice);
            this.bootsText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item2.Name, this.item2.Defence, this.item2.BuyingPrice);
            this.glovesText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item3.Name, this.item3.Defence, this.item3.BuyingPrice);
            this.helmetText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item4.Name, this.item4.Defence, this.item4.BuyingPrice);
            this.robeText = String.Format("{0}" + "\n" + "Def.{1} / {2} Coins", this.item5.Name, this.item5.Defence, this.item5.BuyingPrice);
            this.shieldText = String.Format("{0}" + "\n" + "Atk.{1} / {2} Coins", this.item6.Name, this.item6.Defence, this.item6.BuyingPrice);
            this.staffText = String.Format("{0}" + "\n" + "Atk.{1} / {2} Coins", this.item7.Name, this.item7.Attack, this.item7.BuyingPrice);
        }

        public static MarketDialog Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MarketDialog();
                }
                return instance;
            }
        }

        public void CheckIfSomethingBought(MouseState mouseState, MouseState mouseStatePrevious)
        {
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            if (this.buyButton1Position.Contains(mousePosition)
                && mouseState.LeftButton == ButtonState.Pressed
                && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                int newNumberElixirs = Player.Instance.Elixirs.Count + 1;

                Player.Instance.BuyItem(new Items.ElixirsHP(newNumberElixirs), this.item1.BuyingPrice);
                Handlers.MarketDialogHandler.Instance.IsInMarket = false;
                GameScreen.Instance.GameState = 1;
            }
            else if (this.buyButton2Position.Contains(mousePosition)
                && mouseState.LeftButton == ButtonState.Pressed
                && mouseStatePrevious.LeftButton == ButtonState.Released)
            {
                Player.Instance.BuyItem(this.item2, this.item2.BuyingPrice);
                Handlers.MarketDialogHandler.Instance.IsInMarket = false;
                GameScreen.Instance.GameState = 1;
            }
            else if (this.buyButton3Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item3, this.item3.BuyingPrice);
                Handlers.MarketDialogHandler.Instance.IsInMarket = false;
                GameScreen.Instance.GameState = 1;
            }
            else if (this.buyButton4Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item4, this.item4.BuyingPrice);
                Handlers.MarketDialogHandler.Instance.IsInMarket = false;
                GameScreen.Instance.GameState = 1;
            }
            else if (this.buyButton5Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item5, this.item5.BuyingPrice);
                Handlers.MarketDialogHandler.Instance.IsInMarket = false;
                GameScreen.Instance.GameState = 1;
            }
            else if (this.buyButton6Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item6, this.item6.BuyingPrice);
                Handlers.MarketDialogHandler.Instance.IsInMarket = false;
                GameScreen.Instance.GameState = 1;
            }
            else if (this.buyButton7Position.Contains(mousePosition) && mouseState.LeftButton == ButtonState.Pressed)
            {
                Player.Instance.BuyItem(this.item7, this.item7.BuyingPrice);
                Handlers.MarketDialogHandler.Instance.IsInMarket = false;
                GameScreen.Instance.GameState = 1;
            }

        }

        public void Draw()
        {
            new SystemFunctions.Sprite(dialogWindowTexture, dialogWindowPosition).DrawBox();
            new SystemFunctions.Sprite(this.item1.Texture, item1PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item2.Texture, item2PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item3.Texture, item3PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item4.Texture, item4PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item5.Texture, item5PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item6.Texture, item6PicPosition).DrawBox();
            new SystemFunctions.Sprite(this.item7.Texture, item7PicPosition).DrawBox();

            new SystemFunctions.Sprite(font, this.elixirText, new Vector2(dialogWindowPosition.Left + 60, item1PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.bootsText, new Vector2(dialogWindowPosition.Left + 60, item2PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.glovesText, new Vector2(dialogWindowPosition.Left + 60, item3PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.helmetText, new Vector2(dialogWindowPosition.Left + 60, item4PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.robeText, new Vector2(dialogWindowPosition.Left + 60, item5PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.shieldText, new Vector2(dialogWindowPosition.Left + 60, item6PicPosition.Y + 10)).DrawText();
            new SystemFunctions.Sprite(font, this.staffText, new Vector2(dialogWindowPosition.Left + 60, item7PicPosition.Y + 10)).DrawText();


            new SystemFunctions.Sprite(buyButtontexture, this.buyButton1Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.buyButton2Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.buyButton3Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.buyButton4Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.buyButton5Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.buyButton6Position).DrawBox();
            new SystemFunctions.Sprite(buyButtontexture, this.buyButton7Position).DrawBox();
        }
    }
}
