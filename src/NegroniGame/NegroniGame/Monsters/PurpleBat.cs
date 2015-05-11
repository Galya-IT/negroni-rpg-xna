namespace NegroniGame.Monsters
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class PurpleBat : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP
        public PurpleBat(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Purple Bat", GameScreen.Instance.MonstersTextures[5], 100)
        { }
    }
}
