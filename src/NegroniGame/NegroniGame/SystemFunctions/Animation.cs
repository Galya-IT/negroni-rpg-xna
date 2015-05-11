namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        private const int SWITCH_FRAME = 260;

        private int frame;
        //private bool active;

        private Vector2 position;
        private Vector2 amountOfFrames;
        private Rectangle sourceRect;

        public Vector2 CurrentFrame;

        public Animation(Vector2 position1, Vector2 frames1, Texture2D animationImage)
        {
            //this.active = true;
            this.position = position1;
            this.amountOfFrames = frames1;
            this.IsActive = true;
            this.AnimationImage = animationImage;
        }

        public int AnimationSpeed { get; set; }

        public Vector2 Position { get { return this.position; } set { this.position = value; } }

        public int FrameWidth
        {
            get { return (int)(this.AnimationImage.Width / this.amountOfFrames.X); }
        }

        public int FrameHeight
        {
            get { return (int)(this.AnimationImage.Height / this.amountOfFrames.Y); }
        }

        //public bool Active { get { return this.active; } set { this.active = value; } }

        public bool IsActive { get; private set; }

        public Texture2D AnimationImage { get; set; }

        public void Update(GameTime gameTime)
        {
            this.frame += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.frame >= SWITCH_FRAME)
            {
                this.frame = 0;
                this.CurrentFrame.X += this.FrameWidth;
                if (this.CurrentFrame.X >= this.AnimationImage.Width)
                {
                    this.CurrentFrame.X = 0;
                }
            }

            this.sourceRect = new Rectangle((int)this.CurrentFrame.X, (int)this.CurrentFrame.Y * this.FrameHeight, this.FrameWidth, this.FrameHeight);
        }

        public void Draw()
        {
            GameScreen.Instance.SpriteBatch.Draw(this.AnimationImage, this.position, this.sourceRect, Color.White);
        }
    }
}