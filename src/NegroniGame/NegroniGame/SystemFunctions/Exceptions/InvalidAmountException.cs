namespace NegroniGame.SystemFunctions.Exceptions
{
    using System;

    public class InvalidAmountException  : ApplicationException
    {
        private string exMessage;

        public string ExMessage
        {
            get { return exMessage; }
            private set { exMessage = value; }
        }
        
        public InvalidAmountException(string exMessage)
            : base(exMessage)
        {

        }

        public InvalidAmountException(string exMessage, Exception innerEx)
            : base(exMessage, innerEx)
        {

        }

    }
}
