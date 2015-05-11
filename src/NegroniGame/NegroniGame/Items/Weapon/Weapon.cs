namespace NegroniGame.Items.Weapon
{
    using Microsoft.Xna.Framework.Graphics;
    using System;

    public abstract class Weapon : Interfaces.IWeapon
    {
        private string name;
        private int attack;
        private int buyingPrice;

        public Weapon()
        { }

        public Weapon(string name, int attack, int price, Texture2D texture)
        {
            this.Name = name;
            this.Attack = attack;
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
        public int Attack
        {
            get
            {
                return this.attack;
            }
            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.attack = value;
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
