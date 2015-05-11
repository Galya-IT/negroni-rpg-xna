namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;

    public sealed class Well
    {
        // Singleton !
        private static Well instance;

        //private con-st float REUSE_TIME = 15;
        private float elapsedTimeToReuse = GameSettings.WELL_REUSE_TIME;
        private float elapsedTimeLastMsg;
        private readonly Texture2D wellGraphic = GameScreen.Instance.AllSceneryTextures[2];

        private Well()
        {
            this.WellPosition = new Rectangle(300, GameScreen.ScreenHeight / 2 + 30, 48, 48);
        }

        public static Well Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Well();
                }
                return instance;
             }
        }

        public void Update(GameTime gameTime)
        {
            this.elapsedTimeToReuse += (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.elapsedTimeLastMsg += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // if Player is near the Well
            if (this.WellPosition.Intersects(new Rectangle(Player.Instance.DestinationPosition.X - 5, Player.Instance.DestinationPosition.Y - 5, 32 + 10, 32 + 10)))
            {

                // if Player clicks Enter
                if (GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter)
                    && GameScreen.Instance.KeyboardStatePrevious.IsKeyUp(Keys.Enter))
                {

                    // if reuse time is elapsed
                    if (this.elapsedTimeToReuse >= GameSettings.WELL_REUSE_TIME)
                    {
                        int restoredPoints = Player.Instance.DrinkFromWell();

                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> Well restored you {0} HP.", restoredPoints), Color.Aquamarine } });

                        this.elapsedTimeToReuse = 0;
                    }
                    else
                    {
                        if (this.elapsedTimeLastMsg >= 1)
                        {
                            Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} seconds to reuse.", GameSettings.WELL_REUSE_TIME - (int)this.elapsedTimeToReuse), Color.Red } });
                            this.elapsedTimeLastMsg = 0;
                        }
                    }
                }
            }
        }

        public void Draw()
        {
            new SystemFunctions.Sprite(this.wellGraphic, this.WellPosition).DrawBox();
        }

        public Rectangle WellPosition { get; private set; }
    }
}
