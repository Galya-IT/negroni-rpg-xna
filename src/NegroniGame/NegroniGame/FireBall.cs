namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using NegroniGame.Handlers;
    using NegroniGame.Interfaces;
    using NegroniGame.Monsters;
    using NegroniGame.SystemFunctions;
    using System;

    public class FireBall : SpriteObjectAnime
    {
        private readonly Vector2 target;
        private Vector2 currentFrame;
        private Vector2 velocity;
        private float moveSpeed;

        public FireBall(Vector2 amountOfFrames, Vector2 position, IMonster targetObj)
        {
            base.Name = "FireBall";
            base.position = position;
            base.AmountOfFrames = amountOfFrames;
            this.target = new Vector2(targetObj.DestinationPosition.X, targetObj.DestinationPosition.Y);

            this.moveSpeed = 120;
            this.currentFrame = Vector2.Zero;
            base.Image = GameScreen.Instance.FireballsTexture;

            base.Animation = new Animation(base.position, base.AmountOfFrames, base.Image);
        }

        public override void Update(GameTime gameTime)
        {
            if (base.isActive == true)
            {
                float distance = Vector2.Distance(new Vector2(this.target.X, this.target.Y), this.position);
                float rotation = (float)Math.Atan(distance);

                Vector2 velocityTmp = Vector2.Subtract(new Vector2(this.target.X, this.target.Y), this.position);
                velocity.X = velocityTmp.X.CompareTo(0) * (float)Math.Sin(rotation);
                velocity.Y = velocityTmp.Y.CompareTo(0) * (float)Math.Sin(rotation);

                currentFrame.Y = 0;

                position.X += velocity.X * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.Y += velocity.Y * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                base.Animation.Position = position;
                currentFrame.X = base.Animation.CurrentFrame.X;
                base.Animation.CurrentFrame = currentFrame;
                base.Animation.Update(gameTime);

                for (int i = 0; i < MonstersHandler.Instance.SpawnedMobs.Count; i++)
                {
                    Monster monster = MonstersHandler.Instance.SpawnedMobs[i];
                    if (monster.DestinationPosition.Intersects(this.ReactRect))
                    {
                        monster.HpPointsCurrent -= 30;
                        MonstersHandler.Instance.MobsHit.Add(i);
                        base.IsDeleted = true;
                    }
                }

                if (currentFrame.X / base.Animation.FrameWidth >= base.AmountOfFrames.X - 1)
                {
                    base.IsDeleted = true;
                }
            }
        }

        public override void Draw()
        {
            //if (base.Animation.IsActive)
            //{
                base.Animation.Draw();
            //}
        }

        public Rectangle ReactRect
        {
            get { return new Rectangle((int)base.Position.X, (int)base.Position.Y, base.Animation.FrameWidth, base.Animation.FrameHeight); }
        }

        public void DoAction<T>(IReact obj)
        {
        }
    }
}