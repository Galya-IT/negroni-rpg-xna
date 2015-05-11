namespace NegroniGame.Handlers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;

    public sealed class MarketDialogHandler
    {
        // Singleton !
        private static MarketDialogHandler instance;

        private MarketDialogHandler()
        {
            IsInMarket = false;
        }

        public static MarketDialogHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MarketDialogHandler();
                }
                return instance;
            }
        }

        public bool IsInMarket { get; set; } // TO DO PRIVATE

        public void Update(MouseState mouseState, MouseState mouseStatePrevious)
        {
            if (IsInMarket == false
                && Player.Instance.DestinationPosition.Intersects(
                    new Rectangle(SceneryHandler.Instance.MarketPosition.X - 5, SceneryHandler.Instance.MarketPosition.Y - 5, SceneryHandler.Instance.MarketPosition.Width + 10, SceneryHandler.Instance.MarketPosition.Height + 10))
                && GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter)
                && GameScreen.Instance.KeyboardStatePrevious.IsKeyUp(Keys.Enter))
            {
                IsInMarket = true;
                GameScreen.Instance.GameState = 2;
            }
            else if (IsInMarket == true)
            {
                MarketDialog.Instance.CheckIfSomethingBought(mouseState, mouseStatePrevious);

                if (GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter)
                    && GameScreen.Instance.KeyboardStatePrevious.IsKeyUp(Keys.Enter))
                {
                    IsInMarket = false;
                    GameScreen.Instance.GameState = 1;
                }
            }
        }

        public void Draw()
        {
            if (IsInMarket)
            {
                MarketDialog.Instance.Draw();
            }
        }

    }
}
