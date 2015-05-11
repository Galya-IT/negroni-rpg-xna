namespace NegroniGame.Monsters
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class RedDragon : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP
        public RedDragon(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Red Dragon", GameScreen.Instance.MonstersTextures[0], 100)
        { }
    }
}
