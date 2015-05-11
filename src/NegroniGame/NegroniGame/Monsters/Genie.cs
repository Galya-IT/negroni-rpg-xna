namespace NegroniGame.Monsters
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class Genie : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP
        public Genie(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Genie", GameScreen.Instance.MonstersTextures[4], 100)
        { }
    }
}
