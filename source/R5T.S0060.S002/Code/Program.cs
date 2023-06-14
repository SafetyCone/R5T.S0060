using System;
using System.Threading.Tasks;

using CliWrap;

using R5T.F0000;


namespace R5T.S0060.S002
{
    class Program
    {
        static async Task Main()
        {
            var writer = Console.Out;

            await Cli.Wrap("ipconfig").ExecuteAsync();

            //InputArgumentOperator.Instance.DescribeTo(writer);
            InputArgumentOperator.Instance.DescribeTo_IncludingZeroth(writer);
        }
    }
}
