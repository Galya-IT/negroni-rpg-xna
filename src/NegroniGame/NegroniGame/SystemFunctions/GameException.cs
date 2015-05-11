namespace NegroniGame.SystemFunctions
{
    using System;

    public class GameException : ApplicationException
    {
        public string ExMessage { get; private set; }

        public GameException(string exMessage)
            : base(exMessage)
        {

        }

        public GameException(string exMessage, Exception innerEx)
            : base(exMessage, innerEx)
        {

        }
    }
}