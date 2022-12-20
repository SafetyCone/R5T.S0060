using System;


namespace R5T.S0060.S003
{
    class Program
    {
        static void Main()
        {
            F0035.LoggingOperator.Instance.InConsoleLoggerContext_Synchronous(
                "R5T.S0060.S003",
                logger =>
                {
                    Operations.Instance.SendMail(logger);
                });
        }
    }
}
