namespace NegroniGame.SystemFunctions.Exceptions
{
    using System;

    public class InvalidNameException : ApplicationException
    {
        private string exMessage;

        public string ExMessage
        {
            get { return exMessage; }
            private set { exMessage = value; }
        }
        

        public InvalidNameException(string exMessage)
            : base(exMessage)
        {

        }

        public InvalidNameException(string exMessage, Exception innerEx)
            : base(exMessage, innerEx)
        {

        }
    }
}
