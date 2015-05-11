namespace NegroniGame.SystemFunctions
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public class Sprite
    {
        private readonly Color tint;
        private readonly Vector2 position;
        private readonly Rectangle destinationBox;
        private readonly Rectangle sourceBox;
        private readonly Texture2D texture;
        private readonly SpriteFont font;
        private readonly string text;

        public Sprite(Texture2D picture, Vector2 place, Color? color = null)
        {
            this.texture = picture;
            this.position = place;
            this.tint = color.HasValue ? color.Value : Color.White;
        }

        public Sprite(Texture2D picture, Rectangle destination, Color? color = null)
        {
            this.texture = picture;
            this.destinationBox = destination;
            this.tint = color.HasValue ? color.Value : Color.White;
        }

        public Sprite(Texture2D picture, Rectangle destination, Rectangle source, Color? color = null)
        {
            this.texture = picture;
            this.destinationBox = destination;
            this.sourceBox = source;
            this.tint = color.HasValue ? color.Value : Color.White;
        }

        public Sprite(SpriteFont textFont, string textString, Vector2 place, Color? color = null)
        {
            this.font = textFont;
            this.text = textString;
            this.position = place;
            this.tint = color.HasValue ? color.Value : Color.White;
        }

        public void Draw()
        {
            GameScreen.Instance.SpriteBatch.Draw(this.texture, this.position, this.tint);
        }
        public void DrawBox()
        {
            GameScreen.Instance.SpriteBatch.Draw(this.texture, this.destinationBox, this.tint);
        }
        public void DrawBoxAnim()
        {
            GameScreen.Instance.SpriteBatch.Draw(this.texture, this.destinationBox, this.sourceBox, this.tint);
        }
        public void DrawText()
        {
            GameScreen.Instance.SpriteBatch.DrawString(this.font, this.text, this.position, this.tint);
        }
    }
}