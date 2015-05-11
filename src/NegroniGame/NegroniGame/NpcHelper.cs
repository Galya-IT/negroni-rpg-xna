namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using NegroniGame.Handlers;
    using NegroniGame.Interfaces;
    using NegroniGame.SystemFunctions;

    public sealed class NpcHelper : SpriteObjectAnime, IReact
    {
        // Singleton!
        private static NpcHelper instance;

        private Vector2 currentFrame;

        private int frameCounter = 0;

        private Rectangle reactRect;

        private NpcHelper()
        {
            base.Name = "NPC Helper";
            base.AmountOfFrames = new Vector2(3, 4);
            base.position = new Vector2(100, 100);
            Initialize();
        }

        public static NpcHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NpcHelper();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            base.Image = GameScreen.Instance.NpcHelperTexture;
            this.currentFrame = Vector2.Zero;

            base.Animation = new Animation(base.position, base.AmountOfFrames, base.Image);

            // base.Animation.AnimationImage = base.Image;

            NpcHelperHandler.AddSprite(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.isActive)
            {
                this.frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (this.frameCounter > GameSettings.ACTIVE_TIME)
                {
                    this.frameCounter = 0;
                    this.isActive = false;
                }

                if (Player.Instance.Direction == DirectionsEnum.East)
                {
                    base.position.X = Player.Instance.DestinationPosition.X - 40;
                    base.position.Y = Player.Instance.DestinationPosition.Y - 40;
                    this.currentFrame.Y = 2;
                }
                else if (Player.Instance.Direction == DirectionsEnum.North)
                {
                    base.position.X = Player.Instance.DestinationPosition.X;
                    base.position.Y = Player.Instance.DestinationPosition.Y + 10;
                    this.currentFrame.Y = 3;
                }
                else if (Player.Instance.Direction == DirectionsEnum.South)
                {
                    base.position.X = Player.Instance.DestinationPosition.X;
                    base.position.Y = Player.Instance.DestinationPosition.Y - 50;
                    this.currentFrame.Y = 0;
                }
                else if (Player.Instance.Direction == DirectionsEnum.West)
                {
                    base.position.X = Player.Instance.DestinationPosition.X + 10;
                    base.position.Y = Player.Instance.DestinationPosition.Y - 40;
                    this.currentFrame.Y = 1;
                }

                this.DrawRect = new Rectangle((int)base.position.X, (int)base.position.Y, (int)(base.Image.Width / base.AmountOfFrames.X), (int)(base.Image.Height / base.AmountOfFrames.Y));
                this.reactRect = new Rectangle(this.DrawRect.X - 100, this.DrawRect.Y - 100, this.DrawRect.Width + 100, this.DrawRect.Height + 100);

                foreach (Monsters.Monster monster in Handlers.MonstersHandler.Instance.SpawnedMobs)
                {
                    if (monster.DestinationPosition.Intersects(reactRect) && this.frameCounter % 600 == 0)
                    {
                        FireBall fireBall = new FireBall(new Vector2(8, 1), new Vector2(this.DrawRect.X + this.DrawRect.Width / 2, this.DrawRect.Y + this.DrawRect.Height / 2), monster);
                        fireBall.isActive = true;
                        NpcHelperHandler.AddSprite(fireBall);

                        break;
                    }
                }

                this.currentFrame.X = base.Animation.CurrentFrame.X;

                base.Animation.Position = base.position;
                base.Animation.CurrentFrame = this.currentFrame;

                base.Animation.Update(gameTime);
            }
            else
            {
                if (GameScreen.Instance.KeyboardState.IsKeyDown(Keys.Enter))
                {
                    this.DrawRect = new Rectangle(Player.Instance.DestinationPosition.X, Player.Instance.DestinationPosition.Y, (int)(base.Image.Width / base.AmountOfFrames.X), (int)(base.Image.Height / base.AmountOfFrames.Y));
                    this.reactRect = new Rectangle(this.DrawRect.X - 10, this.DrawRect.Y - 10, this.DrawRect.Width + 10, this.DrawRect.Height + 10);
                    NpcHelperHandler.DoReaction<ImPlayer>(this);
                    this.reactRect = Rectangle.Empty;
                }
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Animation.Draw();
            }
        }

        public Rectangle ReactRect
        {
            get { return this.reactRect; }
        }

        public void DoAction<T>(IReact obj)
        {
            throw new System.NotImplementedException();
        }
    }
}