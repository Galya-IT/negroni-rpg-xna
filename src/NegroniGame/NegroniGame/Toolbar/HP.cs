namespace NegroniGame.Toolbar
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class HP
    {
        // Singleton !
        private static HP instance;

        public static HP Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HP();
                }
                return instance;
            }
        }

        private HP()
        {
            this.HpTexture = GameScreen.Instance.NegroniHPTextures; // negroniHPfull, negroniHP2of3, negroniHP1of3, negroniHPempty
            this.CurrentHpTexture = this.HpTexture[0];
            this.HpPosition = new Rectangle(385, GameScreen.ScreenHeight - 50, 50, 55);
        }

        public List<Texture2D> HpTexture { get; private set; }
        public Texture2D CurrentHpTexture { get; private set; }
        public Rectangle HpPosition { get; private set; }

        public void Update(GameTime gameTime)
        {
            if (Player.Instance.HpPointsCurrent <= 0)
            {
                this.CurrentHpTexture = this.HpTexture[3];
            }
            else if (Player.Instance.HpPointsCurrent <= GameSettings.HP_POINTS_INITIAL / 3)
            {
                this.CurrentHpTexture = this.HpTexture[2];
            }
            else if (Player.Instance.HpPointsCurrent <= (GameSettings.HP_POINTS_INITIAL / 3) * 2)
            {
                this.CurrentHpTexture = this.HpTexture[1];
            }
            else if (Player.Instance.HpPointsCurrent == GameSettings.HP_POINTS_INITIAL)
            {
                this.CurrentHpTexture = this.HpTexture[0];
            }
        }

        public void Draw()
        {
            new SystemFunctions.Sprite(CurrentHpTexture, HpPosition).DrawBox();
		}
	}
}
