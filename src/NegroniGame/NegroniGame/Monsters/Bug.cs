namespace NegroniGame.Monsters
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Linq;

    public class Bug : Monster, Interfaces.IMonster
    {
        // base: Mob ID, starting Position, Name, Textures, starting HP
        public Bug(int numberOfMob, Rectangle initialMonsterPos)
            : base(numberOfMob, initialMonsterPos, "Bug", GameScreen.Instance.MonstersTextures[3], 100)
        { }
    }
}
