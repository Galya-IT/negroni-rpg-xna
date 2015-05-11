namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticBoots : Armor, Interfaces.IBoots
    {
        public MajesticBoots()
            : base("Majestic Boots", 4, 50, GameScreen.Instance.MajesticSetTextures[0])
        { }
    }
}
