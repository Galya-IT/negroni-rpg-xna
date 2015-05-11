namespace NegroniGame.Items.Armor
{
    using System;

    public class MajesticRobe : Armor, Interfaces.IRobe
    {
        public MajesticRobe()
            : base("Majestic Robe", 10, 200, GameScreen.Instance.MajesticSetTextures[3])
        { }
    }
}
