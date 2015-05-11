namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using NegroniGame.Interfaces;
    using NegroniGame.Handlers;
    using NegroniGame.SystemFunctions;
    using System.Collections.Generic;

    public sealed class NpcSorcerer : SpriteObjectAnime, ImPlayer
    {
        // Singleton!
        private static NpcSorcerer instance;

        private Rectangle reactRect;
        private float elapsedTime;

        private NpcSorcerer()
        {
            base.Name = "NPC Sorcerer";
            base.AmountOfFrames = new Vector2(4, 1);
            base.Position = new Vector2(1, 1);
            Initialize();
        }

        public static NpcSorcerer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NpcSorcerer();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            base.Image = GameScreen.Instance.NpcSorcererTexture;
            base.DrawRect = new Rectangle((int)base.Position.X, (int)base.Position.Y, (int)(base.Image.Width / base.AmountOfFrames.X), (int)(base.Image.Height / base.AmountOfFrames.Y));
            this.reactRect = base.DrawRect;
            this.reactRect.Width += 20;
            this.reactRect.Height += 20;
            elapsedTime = GameSettings.TIME_TO_RENT_AGAIN;

            base.Animation = new Animation(base.Position, base.AmountOfFrames, base.Image);

            NpcHelperHandler.AddSprite(this);
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Player.Instance.DestinationPosition.Intersects(new Rectangle(this.DrawRect.X - 5, this.DrawRect.Y - 5, this.DrawRect.Width + 10, this.DrawRect.Height + 10))
                && GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter) && GameScreen.Instance.KeyboardStatePrevious.IsKeyUp(Keys.Enter))
            {
                if (elapsedTime < GameSettings.TIME_TO_RENT_AGAIN)
                {
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { ">> Come back later.", Color.Pink } });
                }
                else if (Player.Instance.Coins.Amount >= GameSettings.HELPER_RENTING_PRICE)
                {
                    Player.Instance.RentHelper();
                    NpcHelper.Instance.isActive = true;
                    elapsedTime = 0;
                }
                else
                {
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { ">> Not enough coins.", Color.Red } });
                }
            }

            base.Animation.Update(gameTime);
        }

        public override void Draw()
        {
            base.Animation.Draw();
        }

        public Rectangle ReactRect
        {
            get { return this.reactRect; }
        }

        public void DoAction<T>(IReact obj)
        {
            (obj as SpriteObject).isActive = true;
        }
    }
}