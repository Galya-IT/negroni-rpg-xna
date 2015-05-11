namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class SpriteObject
    {
        private string name;
        public bool isActive;
        protected Vector2 position;

        public string Name
        {
            get
            {
                return this.name;
            }
            protected set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new SystemFunctions.Exceptions.InvalidNameException("The name can't be null or empty!");
                }
                this.name = value;
            }
        }

        //public string Name { get; protected set; }
        public Texture2D Image { get; protected set; }
        public Rectangle DrawRect { get; protected set; }
        public Vector2 AmountOfFrames { get; protected set; }
        public Vector2 Position { get { return this.position; } protected set { this.position = value; } }
        public bool IsActive { get { return this.isActive; } protected set { this.isActive = value; } }
        public bool IsDeleted { get; protected set; }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw();
    }
}