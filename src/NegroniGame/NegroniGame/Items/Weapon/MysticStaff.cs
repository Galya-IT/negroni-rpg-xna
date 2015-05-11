namespace NegroniGame.Items.Weapon
{
    using System;

    public class MysticStaff : Weapon, Interfaces.IWeapon
    {
        public MysticStaff()
            : base("Mystic Staff", 30, 200, GameScreen.Instance.MysticStaffTexture)
        {}

    }
}
