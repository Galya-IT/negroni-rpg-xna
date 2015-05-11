namespace NegroniGame.Interfaces
{
    using Microsoft.Xna.Framework.Graphics;

    public interface IItem
    {
        string Name { get; }
        Texture2D Texture { get; }
        int BuyingPrice { get; }
    }
}
