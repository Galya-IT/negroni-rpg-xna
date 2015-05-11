namespace NegroniGame.Monsters
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class GreenOne : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP
        public GreenOne(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Green One", GameScreen.Instance.MonstersTextures[2], 100)
        { }
    }
}
