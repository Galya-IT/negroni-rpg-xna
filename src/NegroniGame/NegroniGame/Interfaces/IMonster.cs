namespace NegroniGame.Interfaces
{
    using Microsoft.Xna.Framework;

    public interface IMonster
    {
        string Name { get; }
        Rectangle DestinationPosition { get; }
    }
}
