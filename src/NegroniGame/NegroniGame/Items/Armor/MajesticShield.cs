namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticShield : Armor, Interfaces.IShield
    {
        public MajesticShield()
            : base("Majestic Shield", 8, 100, GameScreen.Instance.MajesticSetTextures[4])
        { }
    }
}
