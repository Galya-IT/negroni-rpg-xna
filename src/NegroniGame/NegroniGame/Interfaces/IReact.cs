namespace NegroniGame.Interfaces
{
    using Microsoft.Xna.Framework;

    public interface IReact
    {
        Rectangle ReactRect { get; }

        void DoAction<T>(IReact obj);
    }
}