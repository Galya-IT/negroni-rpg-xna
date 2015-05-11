namespace NegroniGame
{
    using System;

    public static class EntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static void Main()
        {
            using (GameScreen game = GameScreen.Instance)
            {
                game.Run();
            }
        }
    }
}

