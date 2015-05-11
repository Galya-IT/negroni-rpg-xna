namespace NegroniGame.Handlers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public sealed class GameOverHandler
    {
        // Singleton !
        private static GameOverHandler instance;

        private readonly static Texture2D gameOverTexture = GameScreen.Instance.GameOverTex;

        private GameOverHandler()
        { }

        public static GameOverHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameOverHandler();
                }
                return instance;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Player.Instance.HpPointsCurrent <= 0)
            {
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> No more Negroni!"), Color.Red } });
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> The Bar closed!"), Color.Red } });
                GameScreen.Instance.GameState = 3;
            }
        }

        public void Draw()
        {
            if (GameScreen.Instance.GameState == 3)
            {
                new SystemFunctions.Sprite(gameOverTexture, new Vector2((GameScreen.ScreenWidth - gameOverTexture.Width) / 2, (GameScreen.ScreenHeight - gameOverTexture.Height) / 2 - 50), Color.Red).Draw();
            }
        }
    }
}
