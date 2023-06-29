using System;
using System.Threading.Tasks;


namespace R5T.S0060.S003
{
    class Program
    {
        static async Task Main()
        {
            await F0035.LoggingOperator.Instance.InConsoleLoggerContext(
                "R5T.S0060.S003",
                async logger =>
                {
                    await Operations.Instance.SendMail(logger);
                });
        }
    }
}
