namespace NegroniGame.Items
{
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public struct Coins
    {
        private int amount;

        
        public int Amount
        {
            get
            {
                return this.amount;
            }
            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.amount = value;
            }
        }
        public Texture2D Texture { get; set; }

        public Coins(int amount) : this()
        {
            this.Amount = amount;
            this.Texture = GameScreen.Instance.CoinsTexture;
        }


    }
}
