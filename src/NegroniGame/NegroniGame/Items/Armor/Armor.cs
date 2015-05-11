namespace NegroniGame.Items.Armor
{
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class Armor : Interfaces.IArmor
    {
        private string name;
        private int defence;
        private int buyingPrice;

        public Armor()
        { }

        public Armor(string name, int defence, int price, Texture2D texture)
        {
            this.Name = name;
            this.Defence = defence;
            this.BuyingPrice = price;
            this.Texture = texture;
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

        public int Defence
        {
            get
            {
                return this.defence;
            }
            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.defence = value;
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
