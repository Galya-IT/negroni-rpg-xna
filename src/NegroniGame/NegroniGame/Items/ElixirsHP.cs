namespace NegroniGame.Items
{
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public struct ElixirsHP : Interfaces.IItem
    {
        //public con-st int RECOVERY_AMOUNT = 30;

        private int count;
        private string name;
        private int buyingPrice;
        

        public ElixirsHP(int count)
            : this()
        {
            this.Count = count;
            this.Name = "HP Elixirs";
            this.Texture = GameScreen.Instance.ElixirsTexture;
            this.BuyingPrice = 15;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new SystemFunctions.Exceptions.InvalidNameException("The name can't be null or empty!");
                }
                this.name = value;
            }
        }
        public int Count
        {
            get
            {
                return this.count;
            }
            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.count = value;
            }
        }

        public int BuyingPrice
        {
            get
            {
                return this.buyingPrice;
            }
            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.buyingPrice = value;
            }
        }

        public Texture2D Texture { get; private set; }
    }
}
